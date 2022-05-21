using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DangeonInf
{
    public class LvlUpInteractable : Interactable
    {
        public AudioSource levelUpSound;
        public override void Interact (PlayerManager playerManager)
        {
            playerManager.uIManager.levelUpWindow.SetActive(true);
            levelUpSound = GetComponent<AudioSource>();
            levelUpSound.Play();
        }
    }
}
