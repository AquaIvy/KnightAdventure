using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KnightAdventure
{
    /// <summary>
    /// 通用Move行为类
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveBehaviour : CharacterBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody;
        private Transform playerTrans;
        private CapsuleCollider2D capsuleCollider;

        [Range(0, 10)]
        public float MoveSpeed = 1;

        [Range(100, 1000)]
        public float JumpForce = 400;

        [SerializeField] private JoystickMovement androidJoystick;


        private float move_speed = 0f;


        protected override void Start()
        {
            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            playerTrans = GetComponent<Transform>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
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
            var ground = IsGround();
            if (ground)
            {
                animator.SetTrigger("jump");
                rigidbody.AddForce(Vector2.up * JumpForce);
            }
        }

        public void Blink()
        {
            animator.SetTrigger("blink");
        }

        #endregion


        public void Idle()
        {
            inputMoving = false;
            move_speed = 0;

            animator.SetFloat("walk", 0);
        }

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

        public void StartMoveAI(float speed)
        {
            inputMoving = true;
            move_speed = speed;
        }

        public void StopMoveAI()
        {
            inputMoving = false;
            move_speed = 0;
        }

        private void Move(float speed)
        {
            animator.SetFloat("walk", Mathf.Abs(speed));
            playerTrans.position += speed * Time.deltaTime * MoveSpeed * Vector3.right;

            spriteRenderer.flipX = speed > 0 ? false : speed < 0 ? true : spriteRenderer.flipX;
        }


        protected override void Update()
        {
            base.Update();

            if (androidJoystick != null && androidJoystick.HorizontalInput() != 0)
            {
                move_speed = androidJoystick.HorizontalInput();
            }

            if (!inputMoving)
            {
                move_speed *= 0.9f;
            }

            Move(move_speed);
        }

        [SerializeField]
        private LayerMask groundLayer = 1 << 8;

        private bool IsGround()
        {
            var point = (Vector2)transform.position + capsuleCollider.offset;
            var size = capsuleCollider.size;
            var direction = CapsuleDirection2D.Vertical;
            var angle = 0f;

            var collider = Physics2D.OverlapCapsule(point, size, direction, angle, groundLayer);
            //var collider = Physics2D.OverlapBox(point, size, angle, groundLayer);
            if (collider != null)
            {
                //Debug.Log("IsGround  true ," + collider.name);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}