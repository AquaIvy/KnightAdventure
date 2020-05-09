using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    public class PlayerController : MonoBehaviour
    {

        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody;
        private Transform playerTrans;

        private CapsuleCollider2D capsuleCollider;


        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            playerTrans = GetComponent<Transform>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }


        void Update()
        {

        }

        /// <summary>
        /// 是否面向正前方（右方）
        /// </summary>
        public bool IsFaceForward { get { return !spriteRenderer.flipX; } }

        /// <summary>
        /// 是否面向后方（左方）
        /// </summary>
        public bool IsFaceBack { get { return spriteRenderer.flipX; } }

        public bool IsOnGround { get { return IsGround(); } }

        [SerializeField]
        private LayerMask groundLayer;

        private bool IsGround()
        {
            var point = (Vector2)transform.position + capsuleCollider.offset;
            var size = capsuleCollider.size;
            var direction = CapsuleDirection2D.Vertical;
            var angle = 0f;

            var collider = Physics2D.OverlapCapsule(point, size, direction, angle, groundLayer);
            if (collider != null)
            {
                //Debug.Log(collider.name);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}