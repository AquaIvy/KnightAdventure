using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightAdventure
{
    public enum CharacterType
    {
        /// <summary>
        /// 未定义的
        /// </summary>
        None,

        /// <summary>
        /// 玩家角色
        /// </summary>
        Player,

        /// <summary>
        /// 友军
        /// </summary>
        Friendly,

        /// <summary>
        /// 敌军
        /// </summary>
        Enemy,

        /// <summary>
        /// 场景
        /// </summary>
        Scene,

        Other,
    }
}
