using Aquaivy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 角色各种Behavior行为的基类
    /// </summary>

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        protected PlayerController player;

        protected Animator animator;
        //private SpriteRenderer spriteRenderer;
        //private new Rigidbody2D rigidbody;
        //private Transform playerTrans;


        protected virtual void Start()
        {
            player = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
            //spriteRenderer = GetComponent<SpriteRenderer>();
            //rigidbody = GetComponent<Rigidbody2D>();
            //playerTrans = GetComponent<Transform>();
        }


        protected virtual void Update()
        {

        }
    }
}