using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 角色控制器，控制各种Behavior行为类
    /// </summary>
    public class PlayerController : MonoBehaviour
    {

        public event EventHandler<PlayerDiedEventArgs> PlayerDied;

        private LifeBehaviour health;
        private MoveBehaviour move;
        private AttackBehaviour attack;

        private SpriteRenderer spriteRenderer;



        public LifeBehaviour Health
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

        public void OnDieAnimationPlayedOver()
        {
            PlayerDied?.Invoke(this, new PlayerDiedEventArgs { Player = this });
        }
    }
}