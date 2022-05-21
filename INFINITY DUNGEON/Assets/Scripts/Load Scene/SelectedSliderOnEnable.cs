using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DangeonInf
{
    public class SelectedSliderOnEnable : MonoBehaviour
    {
       public Slider startSlider;

       private void OnEnable() 
       {
           startSlider.Select();
           startSlider.OnSelect(null);
       }
    }
}
