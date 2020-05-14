using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public class PlayerSelect : MonoBehaviour
    {
        [SerializeField] private PlayerType player;

        private void Start()
        {
#if UNITY_EDITOR
            Create();
#endif

        }

        public void Select(int id) => Select((PlayerType)id);

        public void Select(PlayerType player)
        {
            this.player = player;
        }

        public void Create()
        {
            var playerController = Player.Create(player);
            UnityEngine.Object.DontDestroyOnLoad(playerController.gameObject);

            LoadScene();

            Resources.UnloadUnusedAssets();
        }

        private void LoadScene()
        {
            SceneLoader.LoadSceneBySetting();
        }
    }
}
