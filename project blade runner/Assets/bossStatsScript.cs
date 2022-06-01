using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class bossStatsScript : MonoBehaviour
{
    public int bossPower;
    TMP_Text tmp;
    public GameObject powerText;
    public GameObject powerPos;
    GameObject player;
    [SerializeField] bossHeadScript bossHeadScript;
    Rigidbody[] rb;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("asd");
            GetComponent<Animator>().enabled = false;
            bossHeadScript.enabled = false;
        }
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        tmp = powerText.GetComponent<TextMeshProUGUI>();
    }

    float distance;
    void Update()
    {
        distance = transform.transform.position.z - player.transform.position.z;
        tmp.fontSize = 35 + (350 / distance);

        if (tmp.fontSize >= 120)
        {
            tmp.fontSize = 120;
        }
        Vector3 Pos = Camera.main.WorldToScreenPoint(powerPos.transform.position);
        powerText.transform.position = Pos;
  tmp.text =""+ bossPower;

    }
}
