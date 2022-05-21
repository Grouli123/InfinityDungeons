using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class BodyEquipmentSlotUI : MonoBehaviour
    {
        UIManager uIManager;
        public Image icon;
        BodyEquipment Item;

        private void Awake() 
        {
            uIManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(BodyEquipment bodyEquipment)
        {
            if (bodyEquipment != null)
            {
                Item = bodyEquipment;
                icon.sprite = Item.itemIcon;
                icon.enabled = true;
                gameObject.SetActive(true);
            }
            else
            {
                ClearItem();
            }
        }

        public void ClearItem()
        {
            Item = null;
            icon.sprite = null;
            icon.enabled = false;
        }
    
        public void SelectThisSlot()
        {
            uIManager.bodyEquipmentSlotSelected = true;
        }
    }
}