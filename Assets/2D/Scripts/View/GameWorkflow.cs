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
        [SerializeField] private JoystickMovement joystick;

        private void Start()
        {
            LoadUICanvas();
            InitJoystick();

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void InitJoystick()
        {
            joystick?.gameObject.DontDestroyOnLoad();
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "02_Player")
            {
                uiCanvas.StopGameOverAnimation();
            }

            if (joystick != null && Application.platform == RuntimePlatform.Android)
            {
                if (scene.name.StartsWith("01_") || scene.name.StartsWith("02_"))
                {
                    joystick.gameObject.SetActive(false);
                }
                else
                {
                    joystick.gameObject.SetActive(true);
                }
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
