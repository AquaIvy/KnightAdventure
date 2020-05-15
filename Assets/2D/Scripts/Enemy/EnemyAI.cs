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
        [SerializeField] private Vector2[] patrolPoints;        //巡逻点

        private Character character;

        private void Start()
        {
            character = GetComponent<Character>();

            attack_time = Time.realtimeSinceStartup;
        }

        float attack_time ;

        private void Update()
        {
            if (Time.realtimeSinceStartup - attack_time >= 3f)
            {
                attack_time = Time.realtimeSinceStartup;

                AutoAttack();
                //AutoJump();
                //AutoBlink();
            }
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
    }
}
