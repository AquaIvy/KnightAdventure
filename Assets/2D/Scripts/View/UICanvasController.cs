using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnightAdventure
{
    public class UICanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject gameOver;

        private void Start()
        {
            
        }


        public void PlayStartGameAnimation()
        {

        }

        public void StopStartGameAnimation()
        {

        }

        public void PlayGameOverAnimation()
        {
            gameOver.SetActive(true);
        }

        public void StopGameOverAnimation()
        {
            gameOver.SetActive(false);
        }
    }
}
