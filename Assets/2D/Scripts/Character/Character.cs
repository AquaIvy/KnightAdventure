using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 角色，控制各种Behavior行为类。
    /// （指各种各样的游戏内角色，也可用在非生命体角色上）
    /// </summary>
    public class Character : MonoBehaviour
    {
        public CharacterType Type { get; protected set; } = CharacterType.None;

        private LifeBehaviour health;
        private MoveBehaviour move;
        private AttackBehaviour attack;


        public LifeBehaviour Life
        {
            get
            {
                if (health == null)
                {
                    health = GetComponent<LifeBehaviour>();
                }

                return health;
            }
        }

        public MoveBehaviour Move
        {
            get
            {
                if (move == null)
                {
                    move = GetComponent<MoveBehaviour>();
                }

                return move;
            }
        }

        public AttackBehaviour Attack
        {
            get
            {
                if (attack == null)
                {
                    attack = GetComponent<AttackBehaviour>();
                }

                return attack;
            }
        }

        private SpriteRenderer spriteRenderer;


        /// <summary>
        /// 是否面向正前方（右方）
        /// </summary>
        public bool IsFaceForward { get { return !spriteRenderer.flipX; } }

        /// <summary>
        /// 是否面向后方（左方）
        /// </summary>
        public bool IsFaceBack { get { return spriteRenderer.flipX; } }


        protected virtual void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }


        protected virtual void Update()
        {

        }


    }
}