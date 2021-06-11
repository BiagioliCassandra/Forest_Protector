using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forest_Protector
{
    //Script gérant les mouvements possible du joueurs
    public class PlayerInput : MonoBehaviour
    {
        #region  Variables

        private Vector2 _mouvement = Vector2.zero;
        private bool _jump = false;
        private bool _interaction = false;
        //private bool _dash = false;

        #endregion

        #region Properties

        public Vector2 Mouvement => _mouvement;
        public bool Jump => _jump;
        public bool Interaction => _interaction;
        //public bool Dash => _dash;

        #endregion

        #region Build In Method

        /// <summary>
        /// Udapte Mouvement
        /// </summary>
        void Update()
        {
            _mouvement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _jump = Input.GetButtonDown("Jump");
            _interaction = Input.GetKeyDown(KeyCode.E);
            //_dash = Input.GetButtonDown("Jump");
        }
        #endregion
    }
}