using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 distance;
    [SerializeField] GameObject toFollow;
    float speed;
    void Start()
    {
        distance = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void LateUpdate()
    {
        swordStats swordStats = toFollow.GetComponent<swordStats>();
        speed = swordStats.speed;
        transform.position = Vector3.Slerp(this.transform.position, toFollow.transform.position + distance, 0.9f*speed);
    }
}
