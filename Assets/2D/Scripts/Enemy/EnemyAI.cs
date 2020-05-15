using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    [RequireComponent(typeof(Character))]
    public class EnemyAI : MonoBehaviour
    {
        private Character player;

        private void Start()
        {
            player = GetComponent<Character>();
        }

        float attack_time = 0;

        private void Update()
        {
            if (Time.realtimeSinceStartup - attack_time >= 3f)
            {
                attack_time = Time.realtimeSinceStartup;

                AutoAttack();
            }
        }

        private void AutoAttack()
        {
            player.Attack.Fire1();
        }
    }
}
