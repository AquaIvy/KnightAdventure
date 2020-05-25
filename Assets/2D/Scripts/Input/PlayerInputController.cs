using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KnightAdventure
{
    public class PlayerInputController : MonoBehaviour
    {
        public MoveBehaviour moveBehaviour;
        public AttackBehaviour attackBehaviour;


        private bool inputMoving = false;
        private float move_speed = 0f;

        private void Start()
        {

            var jump = new InputAction(binding: "<Keyboard>/c");
            jump.performed += ctx => moveBehaviour.Jump();
            jump.Enable();


            var attack = new InputAction(binding: "<Keyboard>/x");
            attack.performed += ctx => attackBehaviour.Attack();
            attack.Enable();

            var dash = new InputAction(binding: "<Keyboard>/z");
            dash.performed += ctx => moveBehaviour.Dash();
            dash.Enable();
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Started)
            {
                inputMoving = true;
                move_speed = ctx.ReadValue<Vector2>().x;
            }
            else if (ctx.phase == InputActionPhase.Canceled)
            {
                inputMoving = false;
                move_speed = 0;
            }
        }

        private void Update()
        {
            if (!inputMoving)
            {
                move_speed *= 0.9f;
            }

            moveBehaviour.Move(move_speed);

        }
    }
}
