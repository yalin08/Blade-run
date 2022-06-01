using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class durabSliderScript : MonoBehaviour
{


    public Color colornow, colorToGo;
   public Slider sld;
   public Image fill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fill.color = colornow;
        
            float r=0, g=0, b=0;

       
            r =1-sld.value*sld.value;
            g = sld.value+0.2f;
            b = sld.value-0.1f;

            colornow = new Color(r, g, b);
      

    }
}
