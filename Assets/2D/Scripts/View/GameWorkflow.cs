using Aquaivy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KnightAdventure
{
    public class GameWorkflow : UnitySingleton<GameWorkflow>
    {
        private UICanvasController uiCanvas;

        private void Start()
        {
            LoadUICanvas();

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "02_Player")
            {
                uiCanvas.StopGameOverAnimation();
            }
        }

        private void LoadUICanvas()
        {
            var canvas = ResourcesManager.Create("Prefabs/UICanvas");
            uiCanvas = canvas.GetComponent<UICanvasController>();
            canvas.DontDestroyOnLoad();
        }

        internal void Register(Player player)
        {
            player.Life.CharacterDied += Player_PlayerDied;
        }

        private void Player_PlayerDied(object sender, CharacterDiedEventArgs e)
        {
            Unregister(e.Character as Player);

            //玩家死亡，播放GameOver动画
            uiCanvas.PlayGameOverAnimation();
        }

        internal void Unregister(Player player)
        {
            player.Life.CharacterDied -= Player_PlayerDied;
        }
    }
}
