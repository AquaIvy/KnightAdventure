using Aquaivy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 通用Life行为类
    /// </summary>
    public class LifeBehaviour : CharacterBehaviour
    {
        [Range(0, 100)] [SerializeField] private int hp = 100;


        public event EventHandler<CharacterDiedEventArgs> CharacterDied;


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

        /// <summary>
        /// 角色死亡动画播放完毕，由动画状态机调用
        /// </summary>
        public void OnDieAnimationPlayedOver()
        {
            GameObject.Destroy(gameObject);

            CharacterDied?.Invoke(this, new CharacterDiedEventArgs { Character = character });
        }
    }
}