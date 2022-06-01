using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pommelHiltPosChange : MonoBehaviour
{
    SwordPartsChanger swordPartsChanger;
    // Start is called before the first frame update
    void Start()
    {
        swordPartsChanger=GameObject.FindWithTag("Sword").GetComponent<SwordPartsChanger>();



        hiltLevel = swordPartsChanger.hiltCurrent;
        if (hiltLevel == 3)
        {
            transform.position = pommelPos[1].position;
            transform.rotation = pommelPos[1].rotation;
        }
        else
        {
            transform.position =pommelPos[0].position;
            transform.rotation = pommelPos[0].rotation;
        }
    }
    [SerializeField] Transform[] pommelPos;
    
    int hiltLevel;
    // Update is called once per frame
    void Update()
    {
        hiltLevel = swordPartsChanger.hiltCurrent;
        if (hiltLevel == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, pommelPos[1].position, Time.deltaTime/4);
            transform.rotation = pommelPos[1].rotation;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pommelPos[0].position, Time.deltaTime/4);
            transform.rotation = pommelPos[0].rotation;
        }
           
    }
}
