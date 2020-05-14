using Aquaivy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    public class LifeBehaviour : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody;
        private Transform playerTrans;

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


        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            playerTrans = GetComponent<Transform>();
        }


        void Update()
        {

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
            animator.SetTrigger("die");
        }

        public void OnDieAnimationPlayedOver()
        {
            Player.Destroy();
        }
    }
}