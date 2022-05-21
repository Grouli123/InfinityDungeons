using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangeonInf
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        public EnemyManager enemy;
        public UIEnemyHealthBar enemyHealthBar;
        public AudioSource enemySound;
        public AudioClip[] enemyAudioClip;
        private void Awake() 
        {
            enemy = GetComponent<EnemyManager>();
            enemy.enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        }

        void Start()
        {
            enemySound = GetComponent<AudioSource>();
            maxHelth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHelth;
            enemyHealthBar.SetMaxHealth(maxHelth);
        }

        public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
        {
            base.TakeDamageNoAnimation(physicalDamage, fireDamage);
            enemyHealthBar.SetHealth(currentHealth);
        }

        public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "TakeDamage")
        {
            if(isDead == false)
            {
                base.TakeDamage(physicalDamage, fireDamage, damageAnimation = "TakeDamage");
                enemyHealthBar.SetHealth(currentHealth);
                enemy.enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
                enemySound.clip = enemyAudioClip[1];
                enemySound.Play();
                if(currentHealth <= 0)
                {
                    HandleDeath();
                }
            }       
        }
    
        private void HandleDeath()
        {
            currentHealth = 0;
            enemy.enemyAnimatorManager.PlayTargetAnimation("Death", true);
            isDead = true;
            
            if (isDead == true)
            {   
                enemySound.clip = enemyAudioClip[0];
                enemySound.Play();
                Destroy(gameObject, 3f);
            }
        }
    }
}

