using Aquaivy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public class EditorDeveloplment : MonoBehaviour
    {
        private void Start()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                EditorDevelopCreate();

            }
        }

        private void EditorDevelopCreate()
        {
            GameManager gameManager = GameManager.Instance;

            var playerController = Player.Create(PlayerType.Knight);
            UnityEngine.Object.DontDestroyOnLoad(playerController.gameObject);
        }
    }
}
