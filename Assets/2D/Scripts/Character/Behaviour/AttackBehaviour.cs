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
        [SerializeField] private float castInterval = 0.2f;

        [SerializeField] private Transform m_CasstPosition;

        private bool canAttack = true;
        private bool canCast = true;
        private FireballCreater fireballCreater = new FireballCreater();

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

        public void Cast()
        {
            if (!canCast)
                return;

            StartCoroutine(CastCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            canAttack = false;
            animator.SetBool("Attack", true);
            DetectAttack();

            yield return new WaitForSeconds(attackInterval);

            animator.SetBool("Attack", false);

            yield return new WaitForSeconds(0.5f);
            canAttack = true;
        }

        private IEnumerator CastCooldown()
        {
            canCast = false;
            animator.SetBool("Cast", true);
            fireballCreater.Fire(character, m_CasstPosition);

            yield return new WaitForSeconds(castInterval);

            animator.SetBool("Cast", false);

            yield return new WaitForSeconds(0.5f);
            canCast = true;
        }




        protected override void Update()
        {
            base.Update();
        }


        private void DetectAttack()
        {
            Vector2 offset = new Vector2(1.139406f, -0.5f);
            Vector2 size = new Vector2(1.236866f, 1.45f);

            Rect rect = new Rect(offset, size);


            DamageDetectionUtils.CreateRectDamage(character, rect, transform, 100);
        }

    }
}
