using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Forest_Protector
{
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

        [Header("Variables Movement")]
        private float _horizontal;
        private float _vertical;
        private Vector3 _movement;
        private Vector3 _direction = Vector3.zero;
        private bool _canMoving = true;

/*        [Header("Variables Dash")]
        public float dashTimeMax = 2.0f;
        public float dashSpeed = 200.0f;

        private float _dashTime;*/


        [Header("Variables Jump")]
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private float playerSpeed = 2.0f;
        private float jumpHeight = 1.0f;
        private float gravityValue = -9.81f;

        [Header("Canvas")]
        public TMP_Text manaText;

        [Header("Weapon Properties")]
        public GameObject woodWeapon;

        [Header("Componnent")]
        private Camera _cam;
        private CharacterController _cc;
        private PlayerInput _inputs;
        private Animator _animator;
        #endregion

        #region Build In Methods

        // Start is called before the first frame update
        void Start()
        {
            //Récup Component
            _cam = Camera.main;
            _cc = GetComponent<CharacterController>();
            _inputs = PlayerInput.Instance;
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_cc.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _cc.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            //Si le personnage est autorisé à bouger, il peut se mouvoir et sauter
            if (_canMoving)
            {
                //Locomotion();
                Jump();
                //StartCoroutine(Dash());
            }
            //UpdateAnimations();
        }

        #endregion

        #region  Custom Methods

        /// <summary>
        /// Fonction permettant le déplacement du joueur
        /// </summary>
        void Locomotion()
        {
            if (!_inputs)
            {
                return;
            }

            _horizontal = _inputs.Movement.x;
            _vertical = _inputs.Movement.y;

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
            _movement = _direction.normalized * (moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Fonction gérant le saut, les animations du saut, la force du saut et l'application de la gravité
        /// </summary>
        void Jump()
        {
            // Changes the height position of the player..
            if (_inputs.Jump && _cc.isGrounded)
            {
                _animator.SetBool("Jump", true);
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
            _animator.SetBool("Jump", false);

            playerVelocity.y += gravityValue * Time.deltaTime;
            _cc.Move(playerVelocity * Time.deltaTime);

            /*            if (_cc.isGrounded)
                        {
                            _jump = -gravity * coefGravity;
                            if (_inputs.Jump)
                            {
                                _animator.SetBool("Jump", true);
                                _jump = jumpForce;
                            }
                        }
                        else
                        {
                            _animator.SetBool("Jump", false);
                            _jump -= gravity * Time.deltaTime;
                        }

                        _movement += _jump * Vector3.up * Time.deltaTime;
                        Debug.Log(_movement);

                        _cc.Move(_movement);*/
        }

        /// <summary>
        /// Fonction activant l'arme permettant de voir l'arme lors de l'attaque
        /// </summary>
        void AttackStart()
        {
            if(woodWeapon)
            {
                woodWeapon.SetActive(true);
            }
        }

        /// <summary>
        /// Fonction permettant d'interdire au joueur de bouger pendant les animations d'attaques et de jump idle
        /// </summary>
        void NotMoveStart()
        {
            _canMoving = false;
        }

        /// <summary>
        /// Fonction désactivant l'arme une fois que la frame de l'event de l'animation choisit est passée
        /// </summary>
        void AttackEnd()
        {
            if (woodWeapon)
            {
                woodWeapon.SetActive(false);
            }
        }

        /// <summary>
        /// Fonction permettant au joueur de re-bouger une fois les animations attaques ou jump idle terminées
        /// </summary>
        void NotMoveEnd()
        {
            _canMoving = true;
        }

        /// <summary>
        /// Fonction permettant la maj des animations pendant la partie
        /// </summary>
        void UpdateAnimations()
        {
            if (!_animator) return;

            _animator.SetBool("Grounded", _cc.isGrounded);

            if (!_cc.isGrounded)
            {
                _animator.SetFloat("VerticalSpeed", playerVelocity.y);
            }
            else
            {
                if (_inputs.Attack)
                {
                    if (mana > 0)
                    {
                        _animator.SetTrigger("Attack");
                    }
                    else
                    {
                        manaText.color = Color.red;
                    }  
                }
                else
                {
                    _animator.ResetTrigger("Attack");
                }
            }
            _animator.SetBool("Walk", true);
            _animator.SetFloat("Velocity", _direction.magnitude);
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
        #endregion
    }
}