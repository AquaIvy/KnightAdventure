using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public class PlayerCreater
    {
        public PlayerController Create(PlayerType player)
        {
            string assetPath = string.Empty;

            switch (player)
            {
                case PlayerType.Knight:
                    assetPath = "Prefabs/Player/Knight2";
                    break;
                case PlayerType.Guner:
                    throw new NotImplementedException();
                    break;
                case PlayerType.Wizard:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            if (string.IsNullOrEmpty(assetPath))
            {
                throw new NotImplementedException($"PlayerType {player} Not Implemented");
            }

            var load = Resources.Load<GameObject>(assetPath);
            var ins = GameObject.Instantiate<GameObject>(load, null, false);

            return ins.GetComponent<PlayerController>();
        }
    }

    public enum PlayerType
    {
        /// <summary>
        /// 骑士
        /// </summary>
        Knight,

        /// <summary>
        /// 抢手
        /// </summary>
        Guner,

        /// <summary>
        /// 法师
        /// </summary>
        Wizard,
    }
}
