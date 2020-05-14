using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{

    /// <summary>
    /// 角色出生点
    /// </summary>
    public class PlayerSpawn : MonoBehaviour
    {
        private void Start()
        {
            var player = Player.current;
            if (player == null)
            {
                return;
            }

            player.transform.position = this.transform.position;
            player.transform.rotation = this.transform.rotation;

            var cam = Camera.main;
            var follow = cam.GetComponent<CameraFollow>();
            follow.target = player.transform;
            follow.offset = new Vector3(-4, -4.4f, 10);
        }
    }
}
