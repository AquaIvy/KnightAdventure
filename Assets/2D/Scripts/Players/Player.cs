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

        public static PlayerController Create(PlayerType player)
        {
            var playerController = new PlayerCreater().Create(player);
            current = playerController;

            //这里先用调用，以后改为事件
            GameWorkflow.Instance.Register(current);

            return current;
        }


        public static void Destroy(PlayerController playerController)
        {
            if (playerController == current)
            {
                current = null;
            }

            GameObject.Destroy(playerController.gameObject);

            //这里先用调用，以后改为事件

            GameWorkflow.Instance.Unregister(playerController);
        }
    }
}
