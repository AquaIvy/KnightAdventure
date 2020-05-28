using Aquaivy.Unity;
using Gamekit2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KnightAdventure
{
    /// <summary>
    /// 通用Attack行为类
    /// </summary>
    public class AttackBehaviour : CharacterBehaviour
    {

        [Range(0, 100)]
        public int AttackDamage = 10;

        [Range(0, 100)]
        public int StrikeDamage = 20;

        [Range(0, 10)]
        public int CollideDamage = 5;

        [SerializeField] private float attackInterval = 0.2f;
        private bool canAttack = true;

        protected override void Awake()
        {
            base.Awake();
        }


        public void Attack()
        {
            if (!canAttack)
                return;

            StartCoroutine(AttackCooldown());
            CameraShaker.Shake(0.2f, 0.3f);
        }

        IEnumerator AttackCooldown()
        {
            canAttack = false;
            animator.SetBool("Attack", true);
            DetectAttack();

            yield return new WaitForSeconds(attackInterval);

            animator.SetBool("Attack", false);

            yield return new WaitForSeconds(0.5f);
            canAttack = true;
        }

        //public void Fire2()
        //{
        //    animator.SetTrigger("strike");

        //    //这里需要判断，当走的时候才能去做检测
        //    DetectStrike();
        //}



        protected override void Update()
        {
            base.Update();
        }


        private void DetectAttack()
        {
            Vector2 offset = new Vector2(1.139406f, -0.5f);
            Vector2 size = new Vector2(1.236866f, 1.45f);

            Rect rect = new Rect(offset, size);


            DamageDetectionUtils.CreateRectDamage(character, rect, true, 100);
        }

        //private void DetectStrike()
        //{
        //    Vector2 offsetForward = new Vector2(1.139406f, -0.5f);
        //    Vector2 backForward = new Vector2(-1.14f, -0.5f);
        //    Vector2 detectionSize = new Vector2(1.236866f, 1.45f);

        //    if (character.IsFaceForward)
        //    {
        //        DamageDetectionUtils.CreateRectDamage(character, new Rect(offsetForward, detectionSize), 100);
        //    }
        //    else
        //    {
        //        DamageDetectionUtils.CreateRectDamage(character, new Rect(backForward, detectionSize), 100);
        //    }
        //}

    }
}
