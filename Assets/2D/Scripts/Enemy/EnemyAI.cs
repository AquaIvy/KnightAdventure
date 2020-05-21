using Aquaivy.Core.Logs;
using Aquaivy.Core.Utilities;
using Aquaivy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 通用敌人AI
    /// </summary>
    /// <remarks>
    /// 用于有Move和Attack能力的敌方角色.
    /// 
    /// 1.具有路面探测能力，如果前方没有路了，会回头，不会继续走下去
    /// 2.发现玩家后，可对玩家进行追击
    /// </remarks>
    [RequireComponent(typeof(Character))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("Sight 视线")]
        [SerializeField] private Vector2 sightLineOffset;       //视线相对于自身中心的偏移
        [SerializeField] private float sightLineLength = 2;       //视线长度
        [SerializeField] bool isMoveForward = true;


        private Character character;

        private Queue<float> queue_position_x = new Queue<float>(32);

        private void Start()
        {
            character = GetComponent<Character>();

            attack_time = Time.realtimeSinceStartup;
        }

        float attack_time;
        int currentPatrolPointsIdx = 0;

        private void Update()
        {
            if (isMoveBlocking)
            {
                return;
            }

            bool ground_is_ok = GroundCheck();
            //Log.Info("GroundCheck()  " + ground_is_ok);

            if (ground_is_ok)
            {
                character.Move.StartMoveAI(isMoveForward ? 1 : -1);
            }
            else
            {
                isMoveForward = !isMoveForward;
            }

            MoveBlockCheck();
        }

        bool isMoveBlocking = false;
        private void MoveBlockCheck()
        {
            if (queue_position_x.Count >= 32)
                queue_position_x.Dequeue();

            queue_position_x.Enqueue(character.transform.position.x);

            if (queue_position_x.Count >= 32 && queue_position_x.Max() - queue_position_x.Min() < 0.1f)
            {
                if (RandomUtils.RandomBool())
                {
                    isMoveBlocking = true;
                    character.Move.Idle();

                    DelayTask.Invoke(() =>
                    {
                        isMoveBlocking = false;
                        queue_position_x.Clear();
                    }, RandomUtils.RandomMinMax(500, 2000));
                }
                else
                {
                    GoToNextPatrolPoint();
                }

            }
        }

        private void GoToNextPatrolPoint()
        {
            //currentPatrolPointsIdx++;
            //if (currentPatrolPointsIdx >= patrolPoints.Length)
            //{
            //    currentPatrolPointsIdx = 0;
            //}

            isMoveForward = !isMoveForward;
        }

        private void AutoAttack()
        {
            character.Attack.Fire1();
        }

        private void AutoJump()
        {
            character.Move.Jump();
        }

        private void AutoBlink()
        {
            character.Move.Blink();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var character = collision.gameObject.GetComponent<Character>();
            if (character == null)
                return;

            if (this.character.Type == CharacterType.Enemy)
                GoToNextPatrolPoint();
        }

        /// <summary>
        /// 路面检测，前方是否有路
        /// </summary>
        /// <returns></returns>
        private bool GroundCheck()
        {
            var origin = (Vector2)character.transform.position + sightLineOffset;
            var direction = isMoveForward ? new Vector2(1, -1) : new Vector2(-1, -1);
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");

            //Debug.DrawLine(origin, origin + direction * distance, Color.red);
            Debug.DrawRay(origin, direction * sightLineLength, Color.red);

            var hit = Physics2D.Linecast(origin, origin + direction * sightLineLength, layerMask);
            if (hit)
            {

            }

            return hit.collider != null;
        }
    }
}
