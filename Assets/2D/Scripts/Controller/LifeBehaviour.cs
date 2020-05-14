using Aquaivy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    public class LifeBehaviour : PlayerBehaviour
    {
        [Range(0, 100)] [SerializeField] private int hp = 100;


        public int Hp
        {
            get { return hp; }
            set
            {
                hp = value;

                if (hp <= 0)
                {
                    hp = 0;

                    Die();
                }
            }
        }


        protected override void Start()
        {
            base.Start();
        }


        protected override void Update()
        {
            base.Update();
        }


        /// <summary>
        /// 增加血量
        /// </summary>
        /// <param name="hp"></param>
        public void IncreaseHP(int hp)
        {
            Hp += hp;
        }

        /// <summary>
        /// 减少血量
        /// </summary>
        /// <param name="hp"></param>
        public void ReduceHP(int hp)
        {
            Hp -= hp;
        }

        public void Die()
        {
            animator?.SetTrigger("die");
        }

        public void OnDieAnimationPlayedOver()
        {
            GameObject.Destroy(gameObject);
            player.OnDieAnimationPlayedOver();
        }
    }
}