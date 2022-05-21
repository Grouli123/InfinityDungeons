using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangeonInf
{
    public class BombDamageCollider : DamageCollider
    {
        [Header("Explosive Damage & Radius")]
        public int eplosiveRadius = 1;
        public int explosiveDamage;
        public int fireExplosionDamage;

        public Rigidbody bombRigidBody;
        private bool hasCollided = false;
        public GameObject imactParticles;

        protected override void Awake()
        {
           damageCollider = GetComponent<Collider>();
           bombRigidBody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision) 
        {
            if (!hasCollided)
            {
                hasCollided = true;
                imactParticles = Instantiate(imactParticles, transform.position, Quaternion.identity);
                Explode();

                CharacterStatsManager character = collision.transform.GetComponent<CharacterStatsManager>();

                if (character != null)
                {
                    character.TakeDamage(0, explosiveDamage);
                }

                Destroy(imactParticles, 5f);
                Destroy(transform.parent.parent.gameObject);
            }
        }

        private void Explode()
        {
            Collider[] characters = Physics.OverlapSphere(transform.position, eplosiveRadius);

            foreach (Collider objectsInExplosion in characters)
            {
                CharacterStatsManager character = objectsInExplosion.GetComponent<CharacterStatsManager>();

                if (character != null)
                {
                    character.TakeDamage(0, fireExplosionDamage);
                }
            }
        }
    }
}