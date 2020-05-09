using Aquaivy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AttackDetection : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        private AttackController controller;


        void Awake()
        {
        }

        private static GameObject load;

        public static void Detect(AttackController controller, Vector2 offset, Vector2 size)
        {
            if (load == null)
                load = Resources.Load<GameObject>("AttackDetection");

            var ins = GameObject.Instantiate<GameObject>(load);
            ins.transform.position = controller.transform.position;

            var attack = ins.GetComponent<AttackDetection>();
            var box = ins.GetComponent<BoxCollider2D>();

            attack.controller = controller;
            attack.boxCollider = box;

            box.offset = offset;
            box.size = size;

            DelayTask.Invoke(() =>
            {
                GameObject.Destroy(ins);
            }, 1000);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"OnTriggerEnter2D  {collision.gameObject.name}");

            controller.RaiseDetected(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log($"OnTriggerExit2D  {collision.gameObject.name}");
        }
    }
}
