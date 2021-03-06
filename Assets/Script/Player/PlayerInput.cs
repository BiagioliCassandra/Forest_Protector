using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forest_Protector
{
    //Script gérant les mouvements possible du joueurs
    public class PlayerInput : MonoBehaviour
    {
        #region  Variables
        private static PlayerInput _instance;
        private Vector2 _movement = Vector2.zero;
        private bool _jump = false;
        private bool _interaction = false;
        private bool _attack = false;
        //private bool _dash = false;

        #endregion

        #region Properties
        public static PlayerInput Instance => _instance;
        public Vector2 Movement => _movement;
        public bool Jump => _jump;
        public bool Interaction => _interaction;
        public bool Attack => _attack;
        //public bool Dash => _dash;

        #endregion

        #region Build In Method
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        /// <summary>
        /// Maj des mouvements et actions du joueur
        /// </summary>
        void Update()
        {
            _movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _jump = Input.GetButtonDown("Jump");
            _attack = Input.GetButtonDown("Fire1");
            _interaction = Input.GetKeyDown(KeyCode.E);
            //_dash = Input.GetButtonDown("Jump");
        }
        #endregion
    }
}