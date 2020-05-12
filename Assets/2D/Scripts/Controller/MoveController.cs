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
    public class MoveController : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody;
        private Transform playerTrans;
        private PlayerController player;

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
            player = GetComponent<PlayerController>();


            //var move = new InputAction(binding: "Move:<Joystick>/stick[Joystick]");
            //move.performed += ctx => Move(ctx);
            //move.Enable();

            var jump = new InputAction(binding: "*/space");
            jump.performed += ctx => Jump(ctx);
            jump.Enable();
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            Debug.Log("Jump");

            var ground = player.IsOnGround;
            if (ground)
            {
                animator.SetTrigger("jump");
                rigidbody.AddForce(Vector2.up * JumpForce);
            }
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            //Debug.Log($"{ctx.ReadValue<Vector2>()}   {ctx.action.bindings}"  );

            Move(ctx.ReadValue<Vector2>().x);
        }

        private void Move(float h)
        {
            animator.SetFloat("walk", Mathf.Abs(h));
            playerTrans.position += h * Time.deltaTime * MoveSpeed * Vector3.right;

            spriteRenderer.flipX = h > 0 ? false : h < 0 ? true : spriteRenderer.flipX;
        }


        void Update()
        {
            //float h = InputManager.GetAxis("Horizontal");

            //animator.SetFloat("walk", Mathf.Abs(h));
            //playerTrans.position += h * Time.deltaTime * MoveSpeed * Vector3.right;

            ////if (h > 0)
            ////    spriteRenderer.flipX = false;
            ////else if (h < 0)
            ////    spriteRenderer.flipX = true;

            //spriteRenderer.flipX = h > 0 ? false : h < 0 ? true : spriteRenderer.flipX;

            //bool jump = InputManager.GetButtonDown("Jump");
            //var ground = player.IsOnGround;
            //if (jump && ground)
            //{
            //    animator.SetTrigger("jump");
            //    rigidbody.AddForce(Vector2.up * JumpForce);
            //}
        }
    }
}