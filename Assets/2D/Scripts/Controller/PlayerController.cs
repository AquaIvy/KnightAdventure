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




        private Vector3 pointTop;
        private Vector3 pointBottom;
        private float radius = 0.5f;
        private float overLapCapsuleOffset = 0.85f;
        [SerializeField]
        private LayerMask ignoreLayer;

        private bool IsGround()
        {
            pointBottom = transform.position + transform.up * radius - transform.up * overLapCapsuleOffset;
            pointTop = transform.position + transform.up * capsuleCollider.size.y - transform.up * radius;
            LayerMask ignoreMask = ~ignoreLayer;

            var colliders = Physics.OverlapCapsule(pointBottom, pointTop, radius, ignoreMask);
            Debug.DrawLine(pointBottom, pointTop, Color.green);
            if (colliders.Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}