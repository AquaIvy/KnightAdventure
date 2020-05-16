using Aquaivy.Core.Logs;
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
        [SerializeField] private Vector2 sightLineLength;       //视线长度


        //[SerializeField] private Vector2[] patrolPoints;        //巡逻点

        private Character character;

        private bool isMoveForward = true;

        private void Start()
        {
            character = GetComponent<Character>();

            attack_time = Time.realtimeSinceStartup;
        }

        float attack_time;
        int currentPatrolPointsIdx = 0;

        private void Update()
        {
            //if (patrolPoints.Length <= 0)
            //    return;

            //var point = patrolPoints[currentPatrolPointsIdx];
            //if (Vector2.Distance(point, (Vector2)transform.position) < 1)
            //{
            //    GoToNextPatrolPoint();
            //}
            //else if (transform.position.x > point.x)
            //{
            //    character.Move.StartMoveAI(-1);
            //}
            //else if (transform.position.x < point.x)
            //{
            //    character.Move.StartMoveAI(1);
            //}
            //else
            //{
            //    character.Move.StopMoveAI();
            //}

            bool ground_is_ok = GroundCheck();
            Log.Info("GroundCheck()  " + ground_is_ok);

            if (ground_is_ok)
            {
                character.Move.StartMoveAI(isMoveForward ? 1 : -1);
            }
            else
            {
                isMoveForward = !isMoveForward;
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
            float distance = 2f;
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");

            //Debug.DrawLine(origin, origin + direction * distance, Color.red);
            Debug.DrawRay(origin, direction * distance, Color.red);

            var hit = Physics2D.Linecast(origin, origin + direction * distance, layerMask);
            if (hit)
            {

            }

            return hit.collider != null;
        }
    }
}
