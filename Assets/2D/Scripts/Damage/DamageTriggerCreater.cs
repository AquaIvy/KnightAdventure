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
    public static class DamageTriggerCreater
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

        public static DamageTrigger Attach(Character attacker, Damage.Type type, int damageValue, Rect damageRect, int aliveTime = 100)
        {
            var damage = new Damage
            {
                attacker = attacker,
                type = type,
                Value = damageValue,
            };

            return Attach(attacker.transform, damage, damageRect, aliveTime);
        }

        public static DamageTrigger Attach(Transform followTarget, Damage damage, Rect damageRect, int aliveTime = 100)
        {
            if (load == null)
                load = Resources.Load<GameObject>("RectDamage");

            var ins = GameObject.Instantiate<GameObject>(load);
            ins.transform.SetParent(followTarget, false);
            ins.transform.localPosition = Vector3.zero;

            var dectect = ins.GetComponent<DamageTrigger>();
            dectect.SetData(damage, aliveTime);
            dectect.SetDetectionRect(damageRect);

            return dectect;
        }


        /// <summary>
        /// 创建一个固定位置的伤害区域
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="damageRect"></param>
        /// <param name="aliveTime"></param>
        /// <returns></returns>
        public static DamageTrigger Create(Character attacker, Damage.Type type, int damageValue, Rect damageRect, int aliveTime = 100)
        {
            var damage = new Damage
            {
                attacker = attacker,
                type = type,
                Value = damageValue,
            };

            return Create(damage, damageRect, aliveTime);
        }

        public static DamageTrigger Create(Damage damage, Rect damageRect, int aliveTime = 100)
        {
            if (load == null)
                load = Resources.Load<GameObject>("RectDamage");

            var ins = GameObject.Instantiate<GameObject>(load);
            ins.transform.position = damage.attacker.transform.position;

            var dectect = ins.GetComponent<DamageTrigger>();
            dectect.SetData(damage, aliveTime);
            dectect.SetDetectionRect(damageRect);

            return dectect;
        }
    }
}
