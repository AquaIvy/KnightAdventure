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

        [SerializeField] private JoystickMovement joystickMovement;


        private float move_speed = 0f;


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

            //var jump = new InputAction(binding: "*/space");
            //jump.performed += ctx => Jump(ctx);
            //jump.Enable();
        }

        #region InputSystem输入事件

        public void Jump(InputAction.CallbackContext ctx)
        {
            if (ctx.phase != InputActionPhase.Started)
                return;

            Jump();
        }

        #endregion

        #region 最终输入事件

        public void Jump()
        {
            var ground = player.IsOnGround;
            if (ground)
            {
                animator.SetTrigger("jump");
                rigidbody.AddForce(Vector2.up * JumpForce);
            }
        }

        #endregion

        private bool inputMoving = false;

        public void Move(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Started)
            {
                inputMoving = true;
                move_speed = ctx.ReadValue<Vector2>().x;
            }
            else if (ctx.phase == InputActionPhase.Canceled)
            {
                inputMoving = false;
                move_speed = 0;
            }
        }

        private void Move(float h)
        {
            animator.SetFloat("walk", Mathf.Abs(h));
            playerTrans.position += h * Time.deltaTime * MoveSpeed * Vector3.right;

            spriteRenderer.flipX = h > 0 ? false : h < 0 ? true : spriteRenderer.flipX;
        }


        void Update()
        {
            if (joystickMovement.HorizontalInput() != 0)
            {
                move_speed = joystickMovement.HorizontalInput();
            }

            if (!inputMoving)
            {
                move_speed *= 0.9f;
            }

            Move(move_speed);
        }
    }
}