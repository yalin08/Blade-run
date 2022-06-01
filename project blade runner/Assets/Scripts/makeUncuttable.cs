using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeUncuttable : MonoBehaviour
{
   public float timer=1f;
    float timersec;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timersec = timer;
        if (transform.localScale.z > 30)
        {
            rb.AddForce(Random.Range(-15,15),60,30);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timersec -= Time.deltaTime;

        if (timersec > 0.9f)
        {
            gameObject.layer = 0;
        }
        else
            gameObject.layer = 9;

       
        if (timersec <= -1)
        {
            if (transform.localScale.x > 0)
            {
                if (transform.localScale.z > 15)
                {
                    transform.localScale -= Vector3.one * 4;
                }
                else
                transform.localScale -= Vector3.one;

            }
            else
                Destroy(gameObject);
        }


    }
}
