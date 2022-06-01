using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pixelplacement;

public class SliceAnimScript : MonoBehaviour
{
    PlayerMoveScript playerMoveScript;
    swordStats swordStats;
    public float EnemyPower;

    [SerializeField] GameObject Boss;
    public GameObject FindClosestCuttable()
    {
        GameObject[] gos;
        gos=null ;
        gos = GameObject.FindGameObjectsWithTag("cuttable");
        GameObject closest = null;
        float distance = 20f;
        Vector3 position = transform.position;
       
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance&& transform.position.z<go.transform.position.z)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = null;
        gos = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closest = null;
        float distance = 20f;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && transform.position.z < go.transform.position.z)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMoveScript>();
        anim.enabled = false;
        swordStats = GetComponent<swordStats>();
       

    }
    public Animator anim;
    public GameObject closestObject;
    public GameObject closestEnemy;

     float howMuchDurabilityUp;

   public float durabilityObstacle;
    
    // Update is called once per frame
    void Update()
    {
       
        closestObject = FindClosestCuttable();
        closestEnemy = FindClosestEnemy();

        if (Resume)
        {
            Resume = false;
            anim.SetBool("start", false);
            anim.SetBool("Enemy", false); anim.SetBool("Bamboo", false); anim.SetBool("Hay", false);
            anim.enabled = false;
            anim.Rebind()
                ;
            playerMoveScript.isStopped = false;
          

           
           
       
        }

        if (gameObject.transform.position.z >= 35 &&gameObject.transform.position.z<=40)
        {
            Boss.SetActive(true);
        }
          


        

    }
    public bool Resume;
    GameObject alignObject;
    Vector3 alignPos;
    IEnumerator align()
    {
        while (transform.position!=alignPos)
        {
             transform.position = Vector3.Lerp(transform.position,alignPos, 15*Time.deltaTime);
            yield return null;
            if (transform.position == alignPos)
            {
                break;
            }
        }
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject== closestObject)
        {

           
                alignPos = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z - 2.64f);
            alignObject = other.gameObject;
            StartCoroutine("align");
           
            anim.enabled = true;
            playerMoveScript.isStopped = true;

            

           
            if (other.gameObject.layer == 11)
            {

                howMuchDurabilityUp = durabilityObstacle ;
                Invoke("durabilityUp", 1f);
                anim.SetBool("start", true);
                anim.SetBool("Hay", true);
                anim.SetInteger("animNo", 0);
                other.gameObject.layer = 0;

            }
            else if (other.gameObject.layer == 12)
            {
                howMuchDurabilityUp = durabilityObstacle;
                Invoke("durabilityUp", 1f);
                anim.SetBool("start", true);
                anim.SetBool("Bamboo", true);
                anim.SetInteger("animNo", 0);

                other.gameObject.layer = 0;

            }
         
            other.tag = ("Untagged");
        }

        if (other.gameObject.tag == "boss")
        {
            bossHeadScript bossHeadScript = other.GetComponentInChildren<bossHeadScript>();
            bossHeadScript.enabled = false;
            bossHeadScript.animator.enabled = false;
            bossHeadScript.smr.SetBlendShapeWeight(0, 0);
            alignPos = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z - 2.64f);
            alignObject = other.gameObject;
            StartCoroutine("align");

            anim.enabled = true;
            playerMoveScript.isStopped = true;
            alignPos = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z - 4.5f);

            slideMinigame.SetActive(true);
            UICanvas.SetActive(false);

        }
        else if (other.gameObject==closestEnemy && other.tag=="enemy")
        {

            alignPos = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z - 2.64f);
            alignObject = other.gameObject;
            StartCoroutine("align");

            anim.enabled = true;
            playerMoveScript.isStopped = true;

            EnemyPower = other.GetComponent<enemyStats>().EnemyPower;
            bossHeadScript bossHeadScript = other.GetComponentInChildren<bossHeadScript>();
            bossHeadScript.enabled = false;
            bossHeadScript.animator.enabled = false;
            bossHeadScript.smr.SetBlendShapeWeight(0, 0);

            if (swordStats.damage >= EnemyPower)
            {
                howMuchDurabilityUp = durabilityObstacle * ((swordStats.damage - EnemyPower)/ (swordStats.damage - EnemyPower));
                Invoke("durabilityUp", 0.8f);
                anim.SetBool("start", true);

                anim.SetBool("Enemy", true);
                
                other.gameObject.layer = 0;
            }
            else
            {
                anim.SetBool("start", true);

                anim.SetBool("enemyLose", true);

                howMuchDurabilityUp = -durabilityObstacle * ((swordStats.damage - EnemyPower) / (swordStats.damage - EnemyPower));
               
                Invoke("durabilityUp", 1.5f);
                other.gameObject.layer = 0;
                enemyStats enemyStats = other.GetComponent<enemyStats>();
                Collider col = enemyStats.metarig;
               
                col.enabled = false;



                /*  Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

                  foreach (Rigidbody rigidbody in rigidbodies)
                  {
                      rigidbody.isKinematic = false;
                      rigidbody.AddForce(Random.Range(60f, -60f), 0, Random.Range(-170f, 170f));
                  } */

            }
            other.tag = "Untagged";
        }





    }
    [SerializeField] GameObject slideMinigame;
    [SerializeField] GameObject durabilityFillBar;
    [SerializeField] GameObject UICanvas;
    void durabilityUp()
    {
        // Vector3 v3=new Vector3(durabilityFillBar.transform.localScale.x, durabilityFillBar.transform.localScale.y, durabilityFillBar.transform.localScale.z);  
       
        swordStats.durabilityNow += howMuchDurabilityUp;
  

        Tween.LocalScale(durabilityFillBar.transform, new Vector3(1.1f, 1.1f, 1.1f), Vector3.one, 0.6f, 0f, Tween.EaseInOutBack);

    }
 

}
