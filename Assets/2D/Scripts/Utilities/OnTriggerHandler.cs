using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace KnightAdventure
{
    public class OnTriggerHandler : MonoBehaviour
    {
        [System.Serializable]
        public class OnTriggerEnter : UnityEvent { }
        [SerializeField] OnTriggerEnter m_onTriggerEnter;

        private void Start()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            m_onTriggerEnter.Invoke();
        }


    }
}
