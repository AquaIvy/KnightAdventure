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


        private bool inputMoving = false;
        private float move_speed = 0f;

        private void Start()
        {
            //var fire1 = new InputAction(binding: "/{PrimaryAction}");
            //var fire2 = new InputAction(binding: "*/rightButton");

            //fire1.performed += ctx => Fire1(ctx);
            //fire2.performed += ctx => Fire2(ctx);

            //fire1.Enable();
            //fire2.Enable();

            var jump = new InputAction(binding: "<Keyboard>/c");
            jump.performed += ctx => moveBehaviour.Jump();
            jump.Enable();


            //var attack = new InputAction(binding: "<Keyboard>/x");
            //attack.performed += ctx => moveBehaviour.att();
            //attack.Enable();

            var dash = new InputAction(binding: "<Keyboard>/z");
            dash.performed += ctx => moveBehaviour.Dash();
            dash.Enable();


            //var moveRight = new InputAction(binding: "<Keyboard>/rightArrow");
            //moveRight.performed += ctx => moveBehaviour.Move(1f);
            //moveRight.Enable();
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
