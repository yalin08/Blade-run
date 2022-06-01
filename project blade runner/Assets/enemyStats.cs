using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyStats : MonoBehaviour
{
    public int EnemyPower;
    public Collider metarig;
    public GameObject powerText;
    public GameObject powerPos;
    TMP_Text tmp;
    GameObject player;

    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        tmp = powerText.GetComponent<TextMeshProUGUI>();
    }
   public float distance;
    // Update is called once per frame
    void Update()
    {
        distance = transform.transform.position.z - player.transform.position.z;
        tmp.fontSize = 15+(150 / distance ) ;

        if (tmp.fontSize >= 60)
        {
            tmp.fontSize = 60;
        }
      
        
        if (distance <= 15)
        {
            powerText.SetActive(true);
        }
        if (distance > 15)
        {
            powerText.SetActive(false);
        }
        if (distance < 0)
        {
            powerText.SetActive(false);
           
        }
      if (distance < 0)
        {
            destroy();
        }

        
        Vector3 Pos = Camera.main.WorldToScreenPoint(powerPos.transform.position);
        powerText.transform.position = Pos;
       tmp.text = ""+EnemyPower;
    }
    public void destroy()
    {
        if (transform.localScale.x >= 0.1f)
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
        }
        else
        Destroy(gameObject);
    }
}
