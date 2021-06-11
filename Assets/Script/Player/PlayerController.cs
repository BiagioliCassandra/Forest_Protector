using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forest_Protector
{
    [RequireComponent(typeof(States))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        public int mana = 50;

        [Header("StatMove")]
        [SerializeField] private float moveSpeed = 5.0f;

        [SerializeField] private float turnSmoothTime = 0.1f;

        [SerializeField] private float turnSmoothVelocity = 0.1f;

        [Header("Variables Mouvement")]
        private float _horizontal;
        private float _vertical;
        private Vector3 _mouvement;
        private Vector3 _direction = Vector3.zero;

/*        [Header("Variables Dash")]
        public float dashTimeMax = 2.0f;
        public float dashSpeed = 200.0f;

        private float _dashTime;*/


        [Header("Variables Jump")]
        private float _jump;

        public float gravity = 9.81f;
        public float jumpForce = 5f;

        [Header("Componnent")]
        private Camera _cam;
        private CharacterController _cc;
        private PlayerInput _inputs;
        private States _states;

        [Header("Collider")]
        public BoxCollider groundedCollider;
        [Tooltip("Layer permettant de sauter")]
        public LayerMask groundLayer;

        #endregion

        #region Build In Methods

        // Start is called before the first frame update
        void Start()
        {
            //Récup Component
            _cam = Camera.main;
            _cc = GetComponent<CharacterController>();
            _inputs = GetComponent<PlayerInput>();
            _states = GetComponent<States>();

            //Verif Collid
            if (!groundedCollider)
            {
                Debug.LogError("no Grounded Collider");
            }
        }

        // Update is called once per frame
        void Update()
        {
            Locomotion();
            Jump();
            //StartCoroutine(Dash());
            SetStates();
        }

        #endregion

        #region  Custom Methods

        /// <summary>
        /// Fonction permettant le déplacement du joueurs
        /// </summary>
        void Locomotion()
        {
            if (!_inputs)
            {
                return;
            }

            _horizontal = _inputs.Mouvement.x;
            _vertical = _inputs.Mouvement.y;

            _direction.Set(_horizontal, 0, _vertical);

            if (_direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg +
                    _cam.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                    ref turnSmoothVelocity, turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                _direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }
            _mouvement = _direction.normalized * (moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Fonction permettant le dash du joueur toutes les secondes. 
        /// </summary>
        /// <returns></returns>
        /*    IEnumerator Dash()
            {
                if (_inputs.Dash)
                {
                    //Lance un dash avec la vitesse de dash choisie et la direction du regard du joueur
                    _mouvement += dashSpeed * _direction * Time.deltaTime;
                }

                _cc.Move(_mouvement);

                yield return new WaitForSeconds(1.0f);
            }*/

        /// <summary>
        /// Fonction permettant au joueur de sauter
        /// </summary>
        void Jump()
        {
            if (_cc.isGrounded)
            {
                _jump = -gravity * gravity;
                if (_inputs.Jump)
                {
                    _jump = jumpForce;
                }
            }
            else
            {
                if (Mathf.Approximately(_jump, 0))
                {
                    _jump = 0f;
                }

                _jump -= gravity * Time.deltaTime;
            }

            _mouvement += _jump * Vector3.up * Time.deltaTime;

            _cc.Move(_mouvement);
        }

        void SetStates()
        {
            if (!_states) return;

            //EN COURS JUMP

            Collider[] colliders = Physics.OverlapBox(groundedCollider.transform.position + groundedCollider.center, groundedCollider.size / 2f, transform.rotation, groundLayer);

            if (colliders.Length > 0)
            {
                if (colliders.Length >= 1) //Pas sur a 100 de cette phase la
                {
                    //Logiquement ici on detecte qu'il y as plus qu'un collider, donc il devrait être grounded
                    Debug.Log("Collider = 1 voir plus");
                    _states.grounded = true;
                }
                else if (colliders.Length < 1)
                {
                    _states.grounded = false;
                    Debug.Log("-1 collider");
                }
            }
            else
            {
                Debug.Log("No Collider detected");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(groundedCollider.transform.position + groundedCollider.center, groundedCollider.size / 2f);
        }
        #endregion

    }
}