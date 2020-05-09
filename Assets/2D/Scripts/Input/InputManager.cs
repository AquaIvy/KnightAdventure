using Aquaivy.Core.Logs;
using Aquaivy.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnightAdventure
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private JoystickMovement Joystick;

        private string buttonDownName;
        [SerializeField] private Button jump;
        [SerializeField] private Button attack;
        [SerializeField] private Button strike;

        private static InputManager Instance;

        private void Start()
        {
            Instance = this;

            jump.onClick.AddListener(() => buttonDownName = "Jump");
            attack.onClick.AddListener(() => buttonDownName = "Fire1");
            strike.onClick.AddListener(() => buttonDownName = "Fire2");
        }

        private void Update()
        {

        }

        private void LateUpdate()
        {
            buttonDownName = string.Empty;
        }

        public static bool GetButtonDown(string buttonName)
        {
            //if (Application.platform == RuntimePlatform.Android ||
            //    Application.platform == RuntimePlatform.WindowsEditor)
            //{
            //    return buttonName == Instance.buttonDownName;
            //}
            //else
            {
                return Input.GetButtonDown(buttonName);
            }
        }

        public static float GetAxis(string axisName)
        {
            //if (Application.platform == RuntimePlatform.Android ||
            //    Application.platform == RuntimePlatform.WindowsEditor)
            //{
            //    return Instance.Joystick.HorizontalInput();
            //}
            //else
            {
                return Input.GetAxis(axisName);
            }
        }
    }
}
