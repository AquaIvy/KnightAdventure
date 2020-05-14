using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public class Player
    {
        public static PlayerController current { get; private set; }

        private LifeBehaviour health;
        private MoveBehaviour move;
        private AttackBehaviour attack;

        public LifeBehaviour Health
        {
            get
            {
                if (health == null && current != null)
                {
                    health = current.GetComponent<LifeBehaviour>();
                }

                return health;
            }
        }

        public MoveBehaviour Move
        {
            get
            {
                if (move == null && current != null)
                {
                    move = current.GetComponent<MoveBehaviour>();
                }

                return move;
            }
        }

        public AttackBehaviour Attack
        {
            get
            {
                if (attack == null && current != null)
                {
                    attack = current.GetComponent<AttackBehaviour>();
                }

                return attack;
            }
        }




        public static PlayerController Create(PlayerType player)
        {
            var playerController = new PlayerCreater().Create(player);
            current = playerController;
            return current;
        }


        public static void Destroy(PlayerController playerController)
        {
            if (playerController == current)
            {
                current = null;
            }

            GameObject.Destroy(playerController.gameObject);
        }
    }
}
