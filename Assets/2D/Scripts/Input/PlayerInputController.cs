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

        private InputAction jump;
        private InputAction attack;
        private InputAction cast;
        private InputAction dash;

        private void Awake()
        {
            jump = new InputAction(binding: "<Keyboard>/c");
            jump.performed += ctx => moveBehaviour.Jump();


            attack = new InputAction(binding: "<Keyboard>/x");
            attack.performed += ctx => attackBehaviour.Attack();

            cast = new InputAction(binding: "<Keyboard>/f");
            cast.performed += ctx => attackBehaviour.Cast();

            dash = new InputAction(binding: "<Keyboard>/z");
            dash.performed += ctx => moveBehaviour.Dash();
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

        private void OnEnable()
        {
            jump.Enable();
            attack.Enable();
            cast.Enable();
            dash.Enable();
        }

        private void OnDisable()
        {
            jump.Disable();
            attack.Disable();
            cast.Disable();
            dash.Disable();
        }


        private void OnDestroy()
        {
            jump.Disable();
            attack.Disable();
            cast.Disable();
            dash.Disable();

            jump.performed -= ctx => moveBehaviour.Jump();
            attack.performed -= ctx => attackBehaviour.Attack();
            cast.performed -= ctx => attackBehaviour.Cast();
            dash.performed -= ctx => moveBehaviour.Dash();
        }
    }
}
