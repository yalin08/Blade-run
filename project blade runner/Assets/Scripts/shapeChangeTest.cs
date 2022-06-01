using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shapeChangeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shapechanger());
    }

    float blendOne;
    float blendTwo=100f;
   public  SkinnedMeshRenderer skinnedMeshRenderer;

    IEnumerator shapechanger()
    {

       while (blendOne < 100f)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);   
            skinnedMeshRenderer.SetBlendShapeWeight(1, blendTwo);
            blendOne += 1;
            if(blendTwo>0)
            blendTwo -= 1;
            yield return null;

            if (blendOne >= 100)
            {
                break;
            }
        }


        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
