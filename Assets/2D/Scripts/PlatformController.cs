using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private GameObject[] androidPlatformInputGameObjects;

        private void Awake()
        {
            bool androidPlatform = Application.platform == RuntimePlatform.Android;

            for (int i = 0; i < androidPlatformInputGameObjects.Length; i++)
            {
                androidPlatformInputGameObjects[i].SetActive(androidPlatform);
            }
        }
    }
}
