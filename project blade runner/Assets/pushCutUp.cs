using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushCutUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Random.Range(-55f,55f), 150, 25);
    }
    public Rigidbody rb;
    // Update is called once per frame
    void Update()
    {
        
    }
}
