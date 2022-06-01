using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleScript : MonoBehaviour
{
    public enum whichPart
    {
        Blade,Guard,Hilt,Pommel
    }

   public int part_int;
  public   whichPart wpart;
   public  int partLevel;
    public SkinnedMeshRenderer self;
    int swordSets;

    int levelwlevel;
    // Start is called before the first frame update
    void Start()
    {
        

       SwordPartsChanger swordPartsChanger= GameObject.FindWithTag("Sword").GetComponent<SwordPartsChanger>();
        swordSets = swordPartsChanger.swordSets;
        partLevel = Random.Range(0, swordSets+1);


       
       
        
     

        switch (part_int)
        {
            case 0:
                levelwlevel = swordPartsChanger.bladeCurrent;

                break;
            case 1:
                levelwlevel = swordPartsChanger.guardCurrent;

                break;
            case 2:
                levelwlevel = swordPartsChanger.hiltCurrent;

                break;
            case 3:
                levelwlevel = swordPartsChanger.pommelCurrent;

                break;

        }

        while(levelwlevel==partLevel)
        partLevel = Random.Range(0, swordSets + 1);



        part_int = (int)wpart;
        for (int i = swordSets; i >= 0; i--)
        {
            self.SetBlendShapeWeight(i, 0);
        }
        self.SetBlendShapeWeight(partLevel, 100);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
