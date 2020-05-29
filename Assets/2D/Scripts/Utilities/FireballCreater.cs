using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public class FireballCreater
    {
        private GameObject load;

        public void Fire(Character character, Transform firePosition)
        {
            if (load == null)
                load = Resources.Load<GameObject>("Prefabs/Fireball");

            var ins = GameObject.Instantiate<GameObject>(load);
            ins.transform.position = firePosition.position;
            bool facingRight = character.Move.FacingRight;

            Vector2 force = firePosition.right;

            if (!facingRight)
            {
                var scale = ins.transform.localScale;
                scale.x *= -1;
                ins.transform.localScale = scale;

                force = firePosition.right * -1;
            }

            var rig = ins.GetComponent<Rigidbody2D>();
            rig.AddForce(force * 300, ForceMode2D.Force);

            Rect rect = new Rect(0.1585348f, 0.009190321f, 0.851881f, 0.5521439f);
            DamageDetectionUtils.CreateRectDamage(character, rect, ins.transform, 10000);
        }
    }
}
