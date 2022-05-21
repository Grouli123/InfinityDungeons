using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangeonInf
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;

        private void OnTriggerEnter(Collider other) 
        {
            PlayerStatsManager playerStatsManager = other.GetComponent<PlayerStatsManager>();

            if(playerStatsManager != null)
            {
                playerStatsManager.TakeDamage(damage, 0);
            }
        }
    }
}
