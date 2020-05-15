using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public class DeathLineController : Character
    {
        protected override void Start()
        {
            base.Start();

            Attack.AttackDamage = int.MaxValue;

            var size = GetComponent<SpriteRenderer>().size;
            DamageDetectionUtils.CreateRectDamage(this, new Rect(size.x / 2f, size.y / 2f, size.x, size.y), 0);
        }
    }
}
