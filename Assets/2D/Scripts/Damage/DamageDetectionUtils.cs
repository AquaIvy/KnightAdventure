using Aquaivy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 用于创建伤害检测区域
    /// </summary>
    public static class DamageDetectionUtils
    {
        private static GameObject load;

        public static void CreateRectDamage(Character character, Rect rect, int aliveTime)
        {
            if (load == null)
                load = Resources.Load<GameObject>("AttackDetection");

            var ins = GameObject.Instantiate<GameObject>(load);
            ins.transform.position = character.transform.position;

            var dectect = ins.GetComponent<DamageDetection>();
            dectect.SetData(character, rect, aliveTime );
        }
    }
}
