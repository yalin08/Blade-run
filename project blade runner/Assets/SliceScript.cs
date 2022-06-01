using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;


public class SliceScript : MonoBehaviour
{
  
    public Material mat;
    public LayerMask mask;
    public bool canCut;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }
    Rigidbody rb;
    // Update is called once per frame
    void Update()
    {
        Collider[] cuttibleObjects = Physics.OverlapBox(transform.position, new Vector3(1f, 0.1f, 0.1f), transform.rotation, mask);

        foreach (Collider objects in cuttibleObjects)
        {
           
            SlicedHull slicedObject = Cut(objects.GetComponent<Collider>().gameObject, mat);
            if (slicedObject != null && canCut)
            {
                GameObject cuttedUp = slicedObject.CreateUpperHull(objects.gameObject, mat);
                GameObject cuttedDown = slicedObject.CreateLowerHull(objects.gameObject, mat);
                //cuttedUp.tag = "Cutted";
                //  cuttedDown.tag = "Cutted";
                cuttedUp.transform.position = objects.transform.position;
                cuttedDown.transform.position = objects.transform.position;




              

                cuttedUp.transform.SetParent(objects.transform.parent);
                cuttedDown.transform.SetParent(objects.transform.parent);
                
               cuttedDown.AddComponent<makeUncuttable>(); 
               cuttedUp.AddComponent<makeUncuttable>(); //cuttedUp.GetComponent<makeUncuttable>().timer = 0.8f;
                AddSomething(cuttedUp);
            
                AddSomething(cuttedDown);

                cuttedUp.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-25,25),Random.Range(50,150),Random.Range(10,25)));

                Destroy(objects.gameObject);


               // cuttedDown.GetComponent<makeUncuttable>().timer = 1.5f;
               
            }
            else
            {
                return;
            }



        }



    }

    public SlicedHull Cut(GameObject obj, Material mat = null)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }

    public void AddSomething(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;

        

        obj.AddComponent<Rigidbody>();

       

        obj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
       
    }
}
