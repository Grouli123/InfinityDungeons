using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    [System.Serializable]
    public class ClassStats
    {
        public string playerClass;

        [Header("Class Stats")]

        public int maxHpLevel;
        public int maxStaminaLevel;
        public int maxFocusLevel;
    }
}
