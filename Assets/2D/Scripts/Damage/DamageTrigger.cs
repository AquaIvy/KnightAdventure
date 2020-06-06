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
    public class DamageTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask m_triggeredLayer = -1;   //-1表示所有层
        [SerializeField] private float aliveTime = 0;               //0表示永远存在
        [SerializeField] private Damage damage;

        public UnityEvent OnTrigger;

        private DelayTask taskAliveCountdown;

        void Awake()
        {
        }

        public void SetData(Damage damage, int aliveTime)
        {
            this.damage = damage;
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
                if (damage.attacker == null || life != damage.attacker.Life)
                {
                    life.ReduceHP(damage.Value);
                }
            }
        }

        private void OnDestroy()
        {
            taskAliveCountdown?.Release();
        }
    }
}
