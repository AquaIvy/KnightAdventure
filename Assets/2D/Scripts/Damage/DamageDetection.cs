using Aquaivy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamageDetection : MonoBehaviour
    {
        private AttackBehaviour attacker;

        void Awake()
        {
        }

        public void SetData(AttackBehaviour attacker, Rect rect, int aliveTime)
        {
            this.attacker = attacker;

            SetDetectionRect(rect);
            AliveCountdown(aliveTime);
        }

        public void SetDetectionRect(Rect rect)
        {
            var box = GetComponent<BoxCollider2D>();
            box.offset = rect.position;
            box.size = rect.size;
        }

        private void AliveCountdown(int aliveTime)
        {
            DelayTask.Invoke(() =>
            {
                GameObject.Destroy(this.gameObject);
            }, aliveTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log($"DamageDetection  attacker={attacker.gameObject.name}    hit={collision.gameObject.name}");

            var life = collision.GetComponent<LifeBehaviour>();
            if (life != null)
            {
                if (collision.GetComponent<AttackBehaviour>() != attacker)
                {
                    life.ReduceHP(attacker.AttackDamage);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //Debug.Log($"OnTriggerExit2D  {collision.gameObject.name}");
        }
    }
}
