using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DangeonInf
{
    public class ClassSelector : MonoBehaviour
    {
        PlayerManager player;
        public TempPlayerSkin tempPlayerSkin;

        [Header("Class Info UI")]
        public Text hpStat;
        public Text staminaStat;
        public Text focusStat;


        [Header("Class Starting Statas")]
        public ClassStats[] classStats;


        [Header("Class Starting Gear")]
        public ClassGear[] classGears;

        private void Awake() 
        {
            player = FindObjectOfType<PlayerManager>();
        }

        private void AssignClassStats(int classChosen)
        {
            player.playerStatsManager.healthLevel = classStats[classChosen].maxHpLevel;
            tempPlayerSkin.healthLevel = classStats[classChosen].maxHpLevel;

            player.playerStatsManager.staminaLevel = classStats[classChosen].maxStaminaLevel;
            tempPlayerSkin.staminaLevel = classStats[classChosen].maxStaminaLevel;

            player.playerStatsManager.focusLevel = classStats[classChosen].maxFocusLevel;
            tempPlayerSkin.focusLevel = classStats[classChosen].maxFocusLevel;

        }

        public void AssignKnightClass()
        {
            AssignClassStats(0);
            player.playerInventoryManager.currentHelmetEquipment = classGears[0].helmetEquipment;
            tempPlayerSkin.tempHelmetEquipment = classGears[0].helmetEquipment;
            player.playerInventoryManager.currentBodyEquipment = classGears[0].bodyEquipment;
            tempPlayerSkin.tempBodyEquipment = classGears[0].bodyEquipment;
            player.playerInventoryManager.currentLegEquipment = classGears[0].legEquipment;
            tempPlayerSkin.tempLegEquipment = classGears[0].legEquipment;
            player.playerInventoryManager.currentHandEquipment = classGears[0].handEquipment;
            tempPlayerSkin.tempHandEquipment = classGears[0].handEquipment;

            player.playerEquipmentManager.EquipAllEquipmentModels();

            hpStat.text = player.playerStatsManager.healthLevel.ToString();
            staminaStat.text =  player.playerStatsManager.staminaLevel.ToString();
            focusStat.text = player.playerStatsManager.focusLevel.ToString();
        }

        public void AssignWizardClass()
        {
            AssignClassStats(1);
            player.playerInventoryManager.currentHelmetEquipment = classGears[1].helmetEquipment;
            tempPlayerSkin.tempHelmetEquipment = classGears[1].helmetEquipment;
            player.playerInventoryManager.currentBodyEquipment = classGears[1].bodyEquipment;
            tempPlayerSkin.tempBodyEquipment = classGears[1].bodyEquipment;
            player.playerInventoryManager.currentLegEquipment = classGears[1].legEquipment;
            tempPlayerSkin.tempLegEquipment = classGears[1].legEquipment;
            player.playerInventoryManager.currentHandEquipment = classGears[1].handEquipment;
            tempPlayerSkin.tempHandEquipment = classGears[1].handEquipment;

            player.playerEquipmentManager.EquipAllEquipmentModels();
            
            hpStat.text = player.playerStatsManager.healthLevel.ToString();
            staminaStat.text =  player.playerStatsManager.staminaLevel.ToString();
            focusStat.text = player.playerStatsManager.focusLevel.ToString();
        }

        public void AssignBarbarianClass()
        {
            AssignClassStats(2);
            player.playerInventoryManager.currentHelmetEquipment = classGears[2].helmetEquipment;
            tempPlayerSkin.tempHelmetEquipment = classGears[2].helmetEquipment;
            player.playerInventoryManager.currentBodyEquipment = classGears[2].bodyEquipment;
            tempPlayerSkin.tempBodyEquipment = classGears[2].bodyEquipment;
            player.playerInventoryManager.currentLegEquipment = classGears[2].legEquipment;
            tempPlayerSkin.tempLegEquipment = classGears[2].legEquipment;
            player.playerInventoryManager.currentHandEquipment = classGears[2].handEquipment;
            tempPlayerSkin.tempHandEquipment = classGears[2].handEquipment;

            player.playerEquipmentManager.EquipAllEquipmentModels();
            
            hpStat.text = player.playerStatsManager.healthLevel.ToString();
            staminaStat.text =  player.playerStatsManager.staminaLevel.ToString();
            focusStat.text = player.playerStatsManager.focusLevel.ToString();
        }
    }
}
