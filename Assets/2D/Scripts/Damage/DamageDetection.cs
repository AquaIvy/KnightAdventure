using Aquaivy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace KnightAdventure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamageDetection : MonoBehaviour
    {
        [SerializeField] private LayerMask m_triggeredLayer = -1;   //-1表示所有层
        [SerializeField] private float aliveTime = 0;               //0表示永远存在
        public UnityEvent OnTrigger;

        private Character attacker;
        private DelayTask taskAliveCountdown;

        void Awake()
        {
        }

        public void SetData(Damage damage, int aliveTime)
        {
            this.attacker = attacker;
            this.aliveTime = aliveTime;

            if (aliveTime > 0)
            {
                AliveCountdown(aliveTime);
            }
        }

        public void SetDetectionRect(Rect rect)
        {
            var box = GetComponent<BoxCollider2D>();
            box.offset = rect.position;
            box.size = rect.size;
        }


        private void AliveCountdown(int aliveTime)
        {
            taskAliveCountdown?.Release();
            taskAliveCountdown = DelayTask.Invoke(() =>
            {
                if (this.gameObject != null)
                {
                    GameObject.Destroy(this.gameObject);
                }
            }, aliveTime);
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            var triggeredObject = collision.gameObject;

            if (!triggeredObject.layer.IsInLayerMask(m_triggeredLayer))
            {
                return;
            }

            Debug.Log($"DamageDetection    hit={triggeredObject.name}");
            OnTrigger?.Invoke();

            var life = collision.GetComponent<LifeBehaviour>();
            if (life != null)
            {
                if (collision.GetComponent<Character>() != attacker)
                {
                    life.ReduceHP(attacker.Attack.AttackDamage);
                }
            }
        }

        private void OnDestroy()
        {
            taskAliveCountdown?.Release();
        }
    }
}
