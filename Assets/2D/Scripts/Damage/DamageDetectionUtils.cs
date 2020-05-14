using Aquaivy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public static class DamageDetectionUtils
    {
        private static GameObject load;

        public static void CreateRectDamage(AttackBehaviour attacker, Rect rect, int aliveTime)
        {
            if (load == null)
                load = Resources.Load<GameObject>("AttackDetection");

            var ins = GameObject.Instantiate<GameObject>(load);
            ins.transform.position = attacker.transform.position;

            var dectect = ins.GetComponent<DamageDetection>();
            dectect.SetData(attacker, rect, aliveTime * 10);
        }
    }
}
