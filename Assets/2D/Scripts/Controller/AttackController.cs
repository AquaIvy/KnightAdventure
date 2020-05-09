using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KnightAdventure
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class AttackController : MonoBehaviour
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


        void Update()
        {
            bool fire1 = InputManager.GetButtonDown("Fire1");
            bool fire2 = InputManager.GetButtonDown("Fire2");

            if (fire1)
            {
                animator.SetTrigger("attack");
                DetectAttack();
            }

            if (fire2)
            {
                animator.SetTrigger("strike");
                DetectStrike();
            }
        }


        private void DetectAttack()
        {
            Vector2 offsetForward = new Vector2(1.139406f, -0.5f);
            Vector2 backForward = new Vector2(-1.14f, -0.5f);
            Vector2 detectionSize = new Vector2(1.236866f, 1.45f);

            if (player.IsFaceForward)
            {
                AttackDetection.Detect(this, offsetForward, detectionSize);
            }
            else
            {
                AttackDetection.Detect(this, backForward, detectionSize);
            }
        }

        private void DetectStrike()
        {
            Vector2 offsetForward = new Vector2(1.139406f, -0.5f);
            Vector2 backForward = new Vector2(-1.14f, -0.5f);
            Vector2 detectionSize = new Vector2(1.236866f, 1.45f);

            if (player.IsFaceForward)
            {
                AttackDetection.Detect(this, offsetForward, detectionSize);
            }
            else
            {
                AttackDetection.Detect(this, backForward, detectionSize);
            }
        }

        public void RaiseDetected(Collider2D collision)
        {
            var go = collision.gameObject;
            if (go.tag != "Enermy")
            {
                return;
            }

            var health = go.GetComponent<HealthController>();
            health.ReduceHP(AttackDamage);
        }
    }
}
