using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

[System.Serializable]


public class SwordPartsChanger : MonoBehaviour
{
    public int swordSets;


    public SkinnedMeshRenderer part_blade;
    public SkinnedMeshRenderer part_guard;
    public SkinnedMeshRenderer part_hilt;
    public SkinnedMeshRenderer part_pommel;


 
    public bool Resume;
    public void pommelChangeFunction()
    {
       

        StartCoroutine(pommelChange());
    }
    public void hiltChangeFunction()
    {
        
      
        StartCoroutine(hiltChange());
    }
    public void guardChangeFunction()
    {
       
     
        StartCoroutine(guardChange());
    }
    public void bladeChangeFunction()
    {
        

      
        StartCoroutine(bladeChange());
    }
    public int bladeCurrent;
    int bladeBefore=-1;
    int bladePercentUp;
    int bladePercentDown = 100;
    IEnumerator bladeChange()
    {
        
        while (bladePercentUp <= 100)
        {
              for (int i = swordSets; i >= 0; i--)
              {
                  if (i != bladeCurrent) part_blade.SetBlendShapeWeight(i, 0);
              }
        

            part_blade.SetBlendShapeWeight(bladeBefore, bladePercentDown);
            part_blade.SetBlendShapeWeight(bladeCurrent, bladePercentUp);
            bladePercentUp++;
            bladePercentDown--;
          
            yield return null;
             
           }
        if (bladePercentUp >= 100)
        {
            part_blade.SetBlendShapeWeight(bladeCurrent, 100);
            bladePercentUp = 0;
            bladePercentDown = 100;

        }
    }
       public int guardCurrent;
       int guardBefore = -1;
       int guardPercentUp;
       int guardPercentDown = 100;
       IEnumerator guardChange()
       {

         while (guardPercentUp < 100)
         {
            for (int i = swordSets; i >= 0; i--)
             {
                 if (i != guardCurrent)
                     part_guard.SetBlendShapeWeight(i, 0);
             }

           

            part_guard.SetBlendShapeWeight(guardBefore, guardPercentDown);
          part_guard.SetBlendShapeWeight(guardCurrent, guardPercentUp);
          guardPercentUp++;
             guardPercentDown--;


              
        yield return null;
           }
        if (guardPercentUp >= 100)
        {
            part_guard.SetBlendShapeWeight(guardCurrent, 100);
            guardPercentUp = 0;
            guardPercentDown = 100;

        }
    }

       public int hiltCurrent;
       int hiltBefore = -1;
       int hiltPercentUp;
       int hiltPercentDown = 100;

       IEnumerator hiltChange()
       {

           while (hiltPercentUp < 100)
           {

            for (int i = swordSets; i >= 0; i--)
            {
                if (i != hiltCurrent)
                    part_hilt.SetBlendShapeWeight(i, 0);
            }
          

            part_hilt.SetBlendShapeWeight(hiltBefore, hiltPercentDown);
            part_hilt.SetBlendShapeWeight(hiltCurrent, hiltPercentUp);
            hiltPercentUp++;
               hiltPercentDown--;


             
            yield return null;
         }
        if (hiltPercentUp >= 100)
        {
            part_hilt.SetBlendShapeWeight(hiltCurrent, 100);
            hiltPercentUp = 0;
            hiltPercentDown = 100;

        }
    }

     public int pommelCurrent;
     int pommelBefore = -1;
     int pommelPercentUp;
     int pommelPercentDown = 100;
     IEnumerator pommelChange()
     {

         while (pommelPercentUp < 100)
         {

             for (int i = swordSets; i >= 0; i--)
             {
                 if (i != pommelCurrent)
                     part_pommel.SetBlendShapeWeight(i, 0);
             }

            
            part_pommel.SetBlendShapeWeight(pommelBefore, pommelPercentDown);
             part_pommel.SetBlendShapeWeight(pommelCurrent, pommelPercentUp);
             pommelPercentUp++;
              pommelPercentDown--;


             
            yield return null;
        }
         if (pommelPercentUp >= 100)
        {
            part_pommel.SetBlendShapeWeight(pommelCurrent, 100);
            pommelPercentUp = 0;
            pommelPercentDown = 100;

        }
    }

    // Start is called before the first frame update
    void Start()
    {

        swordStats = player.GetComponent<swordStats>();
        for (int i = swordSets; i >= 0; i--)
        {
            part_pommel.SetBlendShapeWeight(i, 0);
            part_guard.SetBlendShapeWeight(i, 0);
            part_hilt.SetBlendShapeWeight(i, 0);
            part_blade.SetBlendShapeWeight(i, 0);
        }
        part_blade.SetBlendShapeWeight(bladeCurrent, 100);
        part_guard.SetBlendShapeWeight(guardCurrent, 100);
        part_hilt.SetBlendShapeWeight(hiltCurrent, 100);
        part_pommel.SetBlendShapeWeight(pommelCurrent, 100);
    }

    public GameObject player;
    swordStats swordStats ;
    [SerializeField] GameObject power;
    void tween()
    {
        Tween.LocalScale(power.transform, new Vector3(1.2f, 1.2f, 1.2f), Vector3.one, 0.6f, 0f, Tween.EaseInOutBack);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "collectible")
        {
            collectibleScript collectibleScript = other.GetComponent<collectibleScript>();

            powerUpScript powerUpScript = other.GetComponent<powerUpScript>();

            tween();

          
                swordStats.damage += damageUp;
           

            Debug.Log("touch");
            int whichpart = collectibleScript.part_int;
            int partlevel = collectibleScript.partLevel;
            switch (whichpart)
            {
                case 0:

                    if (bladeCurrent != partlevel)
                    {


                        bladeBefore = bladeCurrent;
                        bladeCurrent = partlevel;
              

                        bladeChangeFunction();
                    }
                    
                    
                    break; 
                case 1:

                    if (guardCurrent != partlevel)
                    {

                        guardBefore = guardCurrent;guardCurrent = partlevel;
                       
                        guardChangeFunction();
                    }
                    
                   
                    break;
                case 2:

                    if (hiltCurrent != partlevel)
                    {

                        hiltBefore = hiltCurrent;  hiltCurrent = partlevel;

                        hiltChangeFunction();
                    }
                   
                  
                   
                    break;  
                case 3:
                    if (pommelCurrent != partlevel)
                    {

                        pommelBefore = pommelCurrent;  pommelCurrent = partlevel;
                       
                        pommelChangeFunction();
                    }
                  
                    
                    break;



            }

            other.tag = "Untagged";
            other.GetComponentInChildren<Animator>().enabled = false;

            Destroy(touchedPart);
            touchedPart = other.gameObject;
            StartCoroutine("getTouchedPart");

        }
    }
    public int damageUp;
    GameObject touchedPart;

   IEnumerator getTouchedPart()
    {
        while (touchedPart != null && touchedPart.transform.localScale.x > 0.1f)
        {
            Debug.Log("delete?");
            touchedPart.transform.position = Vector3.MoveTowards(touchedPart.transform.position, transform.position,Time.deltaTime*2);
            touchedPart.transform.localScale -= Time.deltaTime*Vector3.one*1.5f;
            yield return null;
        }
        if(touchedPart != null && touchedPart.transform.localScale.x<=0.1f)
            Destroy(touchedPart);

       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
   
}
