using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forest_Protector
{
    public class MagicProjectileAttack : MonoBehaviour
    {
        [Header("Weapon Properties")]
        public GameObject magicProjectile;
        private float _magicProjectileSpeed = 100f;

        private PlayerController _player;
        private GameObject projectile;

        // Start is called before the first frame update
        void Awake()
        {
            _player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            //Si un projectile est d�j� instanci� (ils se d�truisent apr�s 2 secondes de vie)
            //return null et arr�te le programme
            if (projectile != null)
            {
                return;
            }


            //Sinon lance le programme
            if (_player.mana > 0)
            {
                //Instantie le projectile au niveau du baton de mage et le lance dans la direction vers laquelle le player est tourn�
                projectile = Instantiate(magicProjectile, gameObject.transform.position, _player.transform.rotation);

                Rigidbody magicProjectileRb = magicProjectile.GetComponent<Rigidbody>();
                magicProjectileRb.AddForce(Vector3.forward * _magicProjectileSpeed);

                _player.mana -= 10;
            }
            else
            {
                _player.mana = 0;
            }
            _player.manaText.text = "Mana : " + _player.mana + " / 50";
        }
    }
}