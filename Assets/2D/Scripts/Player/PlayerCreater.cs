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
        public Player Create(PlayerType type)
        {
            string assetPath = string.Empty;

            switch (type)
            {
                case PlayerType.Knight:
                    assetPath = "Prefabs/Characters/Knight_New";
                    break;
                case PlayerType.Guner:
                    break;
                case PlayerType.Wizard:
                    break;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(assetPath))
            {
                throw new NotImplementedException($"PlayerType {type} Not Implemented");
            }

            var load = Resources.Load<GameObject>(assetPath);
            var ins = GameObject.Instantiate<GameObject>(load, null, false);

            return ins.GetComponent<Player>();
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
