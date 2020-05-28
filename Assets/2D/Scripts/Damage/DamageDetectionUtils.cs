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

        //public static void CreateRectDamage(Character character, Rect rect, int aliveTime = 100)
        //{
        //    if (load == null)
        //        load = Resources.Load<GameObject>("RectDamage");

        //    var ins = GameObject.Instantiate<GameObject>(load);
        //    ins.transform.position = character.transform.position;

        //    var dectect = ins.GetComponent<DamageDetection>();
        //    dectect.SetData(character, rect, aliveTime);
        //}


        public static DamageDetection CreateRectDamage(Character attacker, Rect damageRect, bool followAttacker = true, int aliveTime = 100)
        {
            if (load == null)
                load = Resources.Load<GameObject>("RectDamage");

            var ins = GameObject.Instantiate<GameObject>(load);
            if (followAttacker && attacker != null)
            {
                ins.transform.SetParent(attacker.transform, false);
                ins.transform.localPosition = Vector3.zero;
            }
            else
            {
                ins.transform.localPosition = Vector3.zero;
            }

            var dectect = ins.GetComponent<DamageDetection>();
            dectect.SetData(attacker, damageRect, aliveTime);

            return dectect;
        }
    }
}
