using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDestroyScript : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(gameObject);
    }
     Rigidbody headrb;
    Animator anim;
    [SerializeField] GameObject self;
    [SerializeField] GameObject head;
    [SerializeField] GameObject body;
    [SerializeField] swipeMechanic swipeMechanic;
    bossStatsScript bossStatsScript;

    private void Start()
    {
        headrb = head.GetComponent<Rigidbody>();
        anim = self.GetComponent<Animator>();
        stoprb();
        if (gameObject.tag == "boss")
        {
            bossStatsScript = self.GetComponent<bossStatsScript>();
        }

    }

    void stoprb()
    {
        headrb.isKinematic = true;
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }
    void startrb()
    {

      
       
        anim.enabled = false;
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        if (gameObject.layer == 10)
        {
            headrb.isKinematic = false;
            headrb.AddForce(Random.Range(-25f, 25f), Random.Range(140f, 160f), Random.Range(-45f, -70f));
            headrb.AddTorque(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            foreach (Rigidbody rigidbody in rigidbodies)
            {


                rigidbody.isKinematic = false;
                rigidbody.AddForce(Random.Range(60f, -60f), 0, Random.Range(-170f, 170f));



            }
            InvokeRepeating("getSmall", 2f, Time.deltaTime);
        }
        else if (gameObject.tag == "boss" && swipeMechanic.didWin == true) 
        {
            bossStatsScript.powerPos.transform.parent = null;
            headrb.isKinematic = false;
            Time.timeScale = 0.4f;
            headrb.AddForce(Random.Range(-50f, 50f), Random.Range(250f, 300f), Random.Range(-50f, -75f));
          headrb.AddTorque(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(-15f, 15f));
            StartCoroutine("bossBodyRb");
            InvokeRepeating("getSmall", 2.5f, Time.deltaTime);

        }
       
     

    }

    
    IEnumerator bossBodyRb()
    {
        yield return new WaitForSeconds(0.2f);
        
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();


        foreach (Rigidbody rigidbody in rigidbodies)
        {

           
                rigidbody.isKinematic = false;
            if (rigidbody != head.GetComponent<Rigidbody>())
            {
                rigidbody.mass = 0.2f;
                rigidbody.AddForce(Random.Range(25f, -25f), 25f, Random.Range(25f, 50f));
            }
            
            


        }



    }
    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            startrb();

        }
    }


    void getSmall()
    {
        if (gameObject.tag == "enemy") { 
            if (transform.localScale.x > 1f)
        {
            
            transform.localScale -= Vector3.one*3;
        }
        else
        {
            Destroy(self);
        }


            if (head.transform.localScale.x > 0.1f)
            {
                head.transform.localScale -= Vector3.one * Time.deltaTime * 3;
            }
        }
else if(gameObject.tag=="boss")
        {
            if (transform.localScale.x > 0.1f)
            {

                transform.localScale -= Vector3.one * 0.1f;
            }
            else
            {
                Destroy(self);
            }


            
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            Debug.Log ("sword decapitation");
            startrb();
        }
    }
}
