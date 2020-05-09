using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KnightAdventure
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveController : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody;
        private Transform playerTrans;

        [Range(0, 10)]
        public float MoveSpeed = 1;

        [Range(0, 300)]
        public float JumpForce = 150;



        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            playerTrans = GetComponent<Transform>();
        }


        void Update()
        {
            float h = InputManager.GetAxis("Horizontal");


            if (h > 0.3f)
            {
                animator.SetBool("walk", true);
                playerTrans.position = playerTrans.position + h * Time.deltaTime * MoveSpeed * Vector3.right;
            }
            else if (h < -0.3f)
            {
                animator.SetBool("walk", true);
                playerTrans.position = playerTrans.position + h * Time.deltaTime * MoveSpeed * Vector3.right;
            }
            else
            {
                animator.SetBool("walk", false);
            }

            if (h > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (h < 0)
            {
                spriteRenderer.flipX = true;
            }

            bool jump = InputManager.GetButtonDown("Jump");
            if (jump)
            {
                animator.SetBool("jump", true);
                rigidbody.AddForce(Vector2.up * JumpForce);
            }
            else
            {
                animator.SetBool("jump", false);
            }
        }
    }
}