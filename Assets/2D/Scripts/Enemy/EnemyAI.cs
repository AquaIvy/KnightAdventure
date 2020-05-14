using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    [RequireComponent(typeof(PlayerController))]
    public class EnemyAI : MonoBehaviour
    {
        private PlayerController player;

        private void Start()
        {
            player = GetComponent<PlayerController>();
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
