using Aquaivy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    public class HealthController : MonoBehaviour
    {

        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody;
        private Transform playerTrans;

        [Range(0, 100)]
        public int HP = 10;

        [Range(0, 100)]
        public int MP = 20;

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

        public void IncreaseHP(int hp)
        {
            HP += hp;
        }

        public void ReduceHP(int hp)
        {
            HP -= hp;

            if (HP <= 0)
            {
                animator.SetBool("die", true);
                //DelayTask.Invoke(() => {
                //    animator.ex
                //}, 100);
            }
        }
    }
}