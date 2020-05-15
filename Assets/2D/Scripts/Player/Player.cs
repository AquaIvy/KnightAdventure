using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 玩家控制的游戏角色
    /// </summary>
    public class Player : Character
    {

        protected override void Start()
        {
            base.Start();
            Type = CharacterType.Player;
        }


        #region static methods

        public static Player current { get; private set; }

        public static Player Create(PlayerType type)
        {
            if (current != null)
            {
                Destroy(current);
            }

            var player = new PlayerCreater().Create(type);
            current = player;

            //这里先用调用，以后改为事件
            GameWorkflow.Instance.Register(current);

            return current;
        }


        public static void Destroy(Player player)
        {
            if (player == current)
            {
                current = null;
            }

            GameObject.Destroy(player.gameObject);

            //这里先用调用，以后改为事件

            GameWorkflow.Instance.Unregister(player);
        }
        #endregion
    }
}
