using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightAdventure
{
    [Serializable]
    public class Damage
    {
        public Character attacker;
        public Type type;
        public int Value;


        public enum Type
        {
            /// <summary>
            /// 一般伤害，遇到物体触发一次伤害后就失效了
            /// </summary>
            General,

            /// <summary>
            /// 穿刺伤害，遇到物体后仍然可进行伤害
            /// </summary>
            Penetrate,
        }
    }
}
