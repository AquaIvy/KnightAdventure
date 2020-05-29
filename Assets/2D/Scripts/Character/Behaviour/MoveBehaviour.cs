using Aquaivy.Core.Logs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace KnightAdventure
{
    /// <summary>
    /// 通用Move行为类
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveBehaviour : CharacterBehaviour
    {
        private new Rigidbody2D rigidbody;



        public float runSpeed = 40f;
        [SerializeField] private float m_JumpForce = 850f;                          // Amount of force added when the player jumps.
        [SerializeField] private float m_DashForce = 25f;
        [Range(0, 0.3f)] [SerializeField] private float m_MovementSmoothing = 0.05f;  // How much to smooth out the movement

        [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
        [SerializeField] private Transform m_WallCheck;                             //Posicion que controla si el personaje toca una pared


        public bool canDoubleJump = true;                                           //If player can double jump
        [SerializeField] private bool m_AirControl = true;                          // Whether or not a player can steer while jumping;
        [SerializeField] private bool m_Invincible = false;                         // 是否无敌

        public bool FacingRight { get { return m_FacingRight; } }


        private bool canMove = true;            // 是否可移动
        private const float k_GroundedCheckRadius = 0.2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private float limitFallSpeed = 25f; // Limit fall speed
        private Vector3 velocity = Vector3.zero;

        private bool canDash = true;
        private bool isDashing = false; //If player is dashing
        private bool m_IsFrontWall = false; //If there is a wall in front of the player


        /// <summary>
        /// 下落过程中每帧触发
        /// </summary>
        [Header("Events")]
        [Space]
        public UnityEvent OnFallEvent;

        /// <summary>
        /// 着陆的一瞬间触发一次
        /// </summary>
        public UnityEvent OnLandEvent;

        [Header("Debug")]
        [Space]
        [SerializeField] private bool debugDrawGroundedCircle = false;

        private float horizontalMove = 0f;
        private bool jump = false;
        private bool dash = false;


        protected override void Awake()
        {
            base.Awake();

            rigidbody = GetComponent<Rigidbody2D>();
            //playerTrans = GetComponent<Transform>();
            //capsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        #region InputSystem输入事件

        /// <summary>
        /// 外界调用Jump方法（由玩家Input调用、敌人AI调用等）
        /// </summary>
        public void Jump()
        {
            jump = true;
        }

        /// <summary>
        /// 外界调用Dash冲刺方法（由玩家Input调用、敌人AI调用等）
        /// </summary>
        public void Dash()
        {
            dash = true;
        }

        /// <summary>
        /// 外界调用Move方法（由玩家Input调用、敌人AI调用等）
        /// </summary>
        public void Move(float speed)
        {
            horizontalMove = speed * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(speed));
        }

        #endregion




        private void FixedUpdate()
        {
            CheckOnTheGround();
            CheckWallFrontCharacter();

            Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
            jump = false;
            dash = false;
        }


        /// <summary>
        /// 检查角色是否面前右面墙（由FixedUpdate调用）
        /// </summary>
        private void CheckWallFrontCharacter()
        {
            m_IsFrontWall = false;

            if (!m_Grounded)
            {
                animator.SetBool("Jump", true);
                OnFallEvent?.Invoke();
                Collider2D[] collidersWall = Physics2D.OverlapCircleAll(m_WallCheck.position, k_GroundedCheckRadius, m_WhatIsGround);
                for (int i = 0; i < collidersWall.Length; i++)
                {
                    if (collidersWall[i].gameObject != null)
                    {
                        isDashing = false;
                        m_IsFrontWall = true;
                    }
                }
            }
        }

        /// <summary>
        /// 检查角色是否在地面上（由FixedUpdate调用）
        /// </summary>
        private void CheckOnTheGround()
        {
            bool wasGrounded = m_Grounded;
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedCheckRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;

                if (!wasGrounded)
                {
                    //上一帧不在地面，但这一帧在地面了，触发“着陆”效果
                    animator.SetBool("Jump", false);
                    animator.SetBool("DoubleJump", false);
                    OnLandEvent?.Invoke();

                    canDoubleJump = true;
                }
            }
        }

        private void OnGUI()
        {
            GUILayout.Box($"m_Grounded={m_Grounded}\tisDashing={isDashing}\tm_IsFrontWall={m_IsFrontWall}\tvelocity={rigidbody.velocity}");

            if (debugDrawGroundedCircle)
            {
                Debug.DrawLine(m_GroundCheck.position - new Vector3(k_GroundedCheckRadius, 0, 0), m_GroundCheck.position + new Vector3(k_GroundedCheckRadius, 0, 0), Color.green);
                Debug.DrawLine(m_GroundCheck.position - new Vector3(0, k_GroundedCheckRadius, 0), m_GroundCheck.position + new Vector3(0, k_GroundedCheckRadius, 0), Color.green);

                GraphicDebug.DrawCircle(m_GroundCheck.position, k_GroundedCheckRadius, Color.yellow);
            }
        }

        public void Move(float move, bool jump, bool dash)
        {
            if (canMove)
            {
                if (dash && canDash)
                {
                    StartCoroutine(DashCooldown());
                }

                if (isDashing)
                {
                    rigidbody.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
                }
                else if ((m_Grounded || m_AirControl))
                {
                    // 限制下落速度
                    if (rigidbody.velocity.y < -limitFallSpeed)
                        rigidbody.velocity = new Vector2(rigidbody.velocity.x, -limitFallSpeed);

                    // Move the character by finding the target velocity
                    Vector3 targetVelocity = new Vector2(move * 10f, rigidbody.velocity.y);


                    // And then smoothing it out and applying it to the character
                    rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

                    if (move > 0 && !m_FacingRight)
                    {
                        Flip();
                    }
                    else if (move < 0 && m_FacingRight)
                    {
                        Flip();
                    }
                }


                if (m_Grounded && jump)
                {
                    // 地面起跳
                    animator.SetBool("Jump", true);
                    m_Grounded = false;
                    rigidbody.AddForce(new Vector2(0f, m_JumpForce));
                    canDoubleJump = true;
                }
                else if (!m_Grounded && jump && canDoubleJump)
                {
                    // 二段跳
                    canDoubleJump = false;
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                    rigidbody.AddForce(new Vector2(0f, m_JumpForce / 1.2f));
                    animator.SetBool("DoubleJump", true);
                }
                else if (!m_Grounded && m_IsFrontWall)
                {
                    // 墙面下滑
                    if (rigidbody.velocity.y <= 0 || isDashing)
                    {
                        //m_WallCheck.localPosition = new Vector3(-m_WallCheck.localPosition.x, m_WallCheck.localPosition.y, 0);
                        //Flip();
                        //canDoubleJump = true;
                        rigidbody.velocity = new Vector2(0, -5);
                    }
                    isDashing = false;

                }
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        /// <summary>
        /// 被击
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="position"></param>
        public void ApplyDamage(float damage, Vector3 position)
        {
            if (!m_Invincible)
            {
                animator.SetBool("Hit", true);

                Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(damageDir * 10);

                //if (life <= 0)
                //{
                //    StartCoroutine(WaitToDead());
                //}
                //else
                {
                    StartCoroutine(Stun(0.25f));
                    StartCoroutine(MakeInvincible(1f));
                }
            }
        }

        IEnumerator DashCooldown()
        {
            animator.SetBool("Dash", true);
            isDashing = true;
            canDash = false;

            yield return new WaitForSeconds(0.15f);
            animator.SetBool("Dash", false);
            isDashing = false;

            yield return new WaitForSeconds(0.5f);
            canDash = true;
        }

        /// <summary>
        /// 设置眩晕时间（不可移动）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator Stun(float time)
        {
            canMove = false;
            yield return new WaitForSeconds(time);
            canMove = true;
        }

        /// <summary>
        /// 设置无敌时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator MakeInvincible(float time)
        {
            m_Invincible = true;
            yield return new WaitForSeconds(time);
            m_Invincible = false;
        }

        //IEnumerator WaitToMove(float time)
        //{
        //    canMove = false;
        //    yield return new WaitForSeconds(time);
        //    canMove = true;
        //}

        //IEnumerator WaitToDead()
        //{
        //    animator.SetBool("IsDead", true);
        //    canMove = false;
        //    m_Invincible = true;
        //    yield return new WaitForSeconds(0.4f);
        //    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        //}

        //public void Idle()
        //{
        //    inputMoving = false;
        //    move_speed = 0;

        //    animator.SetFloat("walk", 0);
        //}

        //private bool inputMoving = false;

        //public void Move(InputAction.CallbackContext ctx)
        //{
        //    if (ctx.phase == InputActionPhase.Started)
        //    {
        //        inputMoving = true;
        //        move_speed = ctx.ReadValue<Vector2>().x;
        //    }
        //    else if (ctx.phase == InputActionPhase.Canceled)
        //    {
        //        inputMoving = false;
        //        move_speed = 0;
        //    }
        //}

        //public void StartMoveAI(float speed)
        //{
        //    inputMoving = true;
        //    move_speed = speed;
        //}

        //public void StopMoveAI()
        //{
        //    inputMoving = false;
        //    move_speed = 0;
        //}

        //private void Move(float speed)
        //{
        //    animator.SetFloat("walk", Mathf.Abs(speed));
        //    playerTrans.position += speed * Time.deltaTime * MoveSpeed * Vector3.right;

        //    spriteRenderer.flipX = speed > 0 ? false : speed < 0 ? true : spriteRenderer.flipX;
        //}


        //protected override void Update()
        //{
        //    base.Update();

        //    //if (androidJoystick != null && androidJoystick.HorizontalInput() != 0)
        //    //{
        //    //    move_speed = androidJoystick.HorizontalInput();
        //    //}

        //    if (!inputMoving)
        //    {
        //        move_speed *= 0.9f;
        //    }

        //    Move(move_speed);
        //}

        //[SerializeField]
        //private LayerMask groundLayer = 1 << 8;

        //private bool IsGround()
        //{
        //    var point = (Vector2)transform.position + capsuleCollider.offset;
        //    var size = capsuleCollider.size;
        //    var direction = CapsuleDirection2D.Vertical;
        //    var angle = 0f;

        //    var collider = Physics2D.OverlapCapsule(point, size, direction, angle, groundLayer);
        //    //var collider = Physics2D.OverlapBox(point, size, angle, groundLayer);
        //    if (collider != null)
        //    {
        //        //Debug.Log("IsGround  true ," + collider.name);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}