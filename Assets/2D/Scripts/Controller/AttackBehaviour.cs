using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KnightAdventure
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class AttackBehaviour : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody;
        private Transform playerTrans;
        private PlayerController player;

        [Range(0, 100)]
        public int AttackDamage = 10;

        [Range(0, 100)]
        public int StrikeDamage = 20;


        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            playerTrans = GetComponent<Transform>();
            player = GetComponent<PlayerController>();
        }

        #region InputSystem输入事件

        public void Fire1(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started)
                return;

            Fire1();
        }

        public void Fire2(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started)
                return;

            Fire2();
        }

        #endregion

        #region 最终输入事件

        public void Fire1()
        {
            animator.SetTrigger("attack");
            DetectAttack();
        }

        public void Fire2()
        {
            animator.SetTrigger("strike");

            //这里需要判断，当走的时候才能去做检测
            DetectStrike();
        }

        #endregion


        void Update()
        {

        }


        private void DetectAttack()
        {
            Vector2 offsetForward = new Vector2(1.139406f, -0.5f);
            Vector2 backForward = new Vector2(-1.14f, -0.5f);
            Vector2 detectionSize = new Vector2(1.236866f, 1.45f);

            if (player.IsFaceForward)
            {
                DamageDetectionUtils.CreateRectDamage(this, new Rect(offsetForward, detectionSize), 100);
            }
            else
            {
                DamageDetectionUtils.CreateRectDamage(this, new Rect(backForward, detectionSize), 100);
            }
        }

        private void DetectStrike()
        {
            Vector2 offsetForward = new Vector2(1.139406f, -0.5f);
            Vector2 backForward = new Vector2(-1.14f, -0.5f);
            Vector2 detectionSize = new Vector2(1.236866f, 1.45f);

            if (player.IsFaceForward)
            {
                DamageDetectionUtils.CreateRectDamage(this, new Rect(offsetForward, detectionSize), 100);
            }
            else
            {
                DamageDetectionUtils.CreateRectDamage(this, new Rect(backForward, detectionSize), 100);
            }
        }

        public void OnTriggerDetected(Collider2D collision)
        {
            var go = collision.gameObject;
            if (go.tag != "Enermy")
            {
                return;
            }

            var health = go.GetComponent<LifeBehaviour>();
            health.ReduceHP(AttackDamage);
        }
    }
}
