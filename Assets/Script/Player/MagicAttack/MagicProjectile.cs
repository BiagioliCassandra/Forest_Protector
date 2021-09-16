using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forest_Protector
{
    public class MagicProjectile : MonoBehaviour
    {
        private PlayerController _player;
        private float _speed = 10f;

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            //Le projectile avance tout droit
            transform.Translate(0, 0, _speed * Time.deltaTime);

            StartCoroutine(DestroyGameObject());
        }

        private void OnTriggerEnter(Collider other)
        {
            //Si le projectile touche un enemy
            if(other.gameObject.layer == 8)
            {
                //Il se détruit
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Détruit le projectile s'il n'a pas touché d'ennemis au bout de 2 secondes
        /// </summary>
        /// <returns>2 secondes de temps</returns>
        IEnumerator DestroyGameObject()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}