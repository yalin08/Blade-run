using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHeadScript : MonoBehaviour
{

    GameObject player;
 [HideInInspector] public  Animator animator;
    public SkinnedMeshRenderer smr;
 
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindWithTag("Sword");

        animator = GetComponent<Animator>();
        Invoke("blink", 3.5f);
    }
    void blink()
    {

        animator.enabled = true;
    }


 

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = player.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rot;





      
    }

   


}
