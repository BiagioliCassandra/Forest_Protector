using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forest_Protector
{
    //Script permettant l'int�raction entre le joueur et l'objet int�ragible
    public class InteragibleObject : MonoBehaviour
    {
        public GameObject player;

        private Canvas playerCanvas;

        private void Start()
        {
            playerCanvas = player.GetComponentInChildren<Canvas>();
            playerCanvas.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == 6)
            {
                playerCanvas.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 6)
            {
                playerCanvas.enabled = false;
            }
        }
    }
}