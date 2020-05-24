using Aquaivy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 所有游戏角色中各种Behavior行为的基类
    /// </summary>

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Character))]
    public class CharacterBehaviour : MonoBehaviour
    {
        protected Character character;

        protected Animator animator;
        //private SpriteRenderer spriteRenderer;
        //private new Rigidbody2D rigidbody;
        //private Transform playerTrans;


        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            character = GetComponent<Character>();
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