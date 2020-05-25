using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace KnightAdventure
{
    public class JoystickInputController : MonoBehaviour
    {
        public MoveBehaviour moveBehaviour;
        public AttackBehaviour attackBehaviour;

        private JoystickMovement joystick;



        private IEnumerator Start()
        {

            yield return new WaitForSeconds(0.5f);

            var go = GameObject.Find("Joystick");
            if (go == null)
            {
                yield break;
            }

            joystick = go.GetComponent<JoystickMovement>();
            if (joystick == null)
            {
                yield break;
            }

            go.transform.Find("Buttons/Jump").GetComponent<Button>().onClick.AddListener(() => moveBehaviour.Jump());
            go.transform.Find("Buttons/Strike").GetComponent<Button>().onClick.AddListener(() => moveBehaviour.Dash());
            go.transform.Find("Buttons/Attack").GetComponent<Button>().onClick.AddListener(() => attackBehaviour.Attack());
        }

        private void Update()
        {
            if (joystick == null)
                return;

            var h = joystick.HorizontalInput();
            moveBehaviour.Move(h);
        }
    }
}
