using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Forest_Protector
{
    public class ChangeColor : MonoBehaviour
    {
        public PlayerController player;

        private PlayerInput _inputs;

        private void Start()
        {
            _inputs = player.GetComponent<PlayerInput>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_inputs.Interaction)
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                player.mana -= 10;
                player.manaText.text = "Mana : " + player.mana + " / 50";
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            }
        }
    }
}