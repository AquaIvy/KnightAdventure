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

            animator.SetFloat("walk", Mathf.Abs(h));
            playerTrans.position += h * Time.deltaTime * MoveSpeed * Vector3.right;

            //if (h > 0)
            //    spriteRenderer.flipX = false;
            //else if (h < 0)
            //    spriteRenderer.flipX = true;

            spriteRenderer.flipX = h > 0 ? false : h < 0 ? true : spriteRenderer.flipX;

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