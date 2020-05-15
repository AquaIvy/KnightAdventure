using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    /// <summary>
    /// 敌方角色
    /// </summary>
    public class Enemy : Character
    {

        protected override void Start()
        {
            base.Start();
            Type = CharacterType.Enemy;
        }
    }
}
