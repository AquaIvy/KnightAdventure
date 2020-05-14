using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        //public int Hp { get; set; }


        private SpriteRenderer spriteRenderer;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
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

    }
}