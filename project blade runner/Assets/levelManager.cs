using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[System.Serializable]
public class Levels
{
    public string levelNo;
    public int[] chunks;
}

public class levelManager : MonoBehaviour
{
    public Levels[] levels;
    public int currentLevel;
    public GameObject[] Chunks;
    Transform chunk;
    [SerializeField] GameObject boss;
    bossStatsScript bossStatsScript;

    // Start is called before the first frame update

 private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.hehe"))
            LoadPlayer();
        else
            SavePlayer();


        Time.timeScale = 1;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
       PlayerData data= SaveSystem.LoadPlayer();
        currentLevel = data.CurrentLevel;
    }


    void Start()
    {

        chunk = GameObject.FindWithTag("chunk").transform;


        bossStatsScript = boss.GetComponent<bossStatsScript>();
        GenerateLevel();


    }

    int spawnedCollectible = 0;
    void GenerateLevel()
    {
        foreach (Transform child in chunk)
        {
            Destroy(child.gameObject);
        }


        if (currentLevel < levels.Length)
            for (int i = 0; i <= 5; i++)
            {


                GameObject Spawned = Instantiate(Chunks[levels[currentLevel].chunks[i]], new Vector3(0, 0, 6 + (i * 11)), Quaternion.identity, chunk);

                //     Spawned.GetComponentInChildren<enemyStats>().EnemyPower=40+spawnedCollectible*10;

                foreach (Transform child in Spawned.transform)
                {
                    if (child.tag == "enemy")
                    {

                        enemyStats enemyStats = child.GetComponent<enemyStats>();
                        enemyStats.EnemyPower = Random.Range(30, 45) + Random.Range(5, 20) * spawnedCollectible;
                        enemyStats.canvas.sortingOrder = -i;
                    }


                    if (child.tag == "collectible")
                        spawnedCollectible++;
                }



            }
        else
            for (int i = 0; i <= 5; i++)
            {
                GameObject Spawned = Instantiate(Chunks[Random.Range(0, Chunks.Length)], new Vector3(0, 0, 6 + (i * 11)), Quaternion.identity, chunk);
                foreach (Transform child in Spawned.transform)
                {
                    if (child.tag == "enemy")
                        child.GetComponent<enemyStats>().EnemyPower = Random.Range(30,45) + Random.Range(5, 20) * spawnedCollectible;

                    if (child.tag == "collectible")
                        spawnedCollectible++;
                }
            }

        bossStatsScript.bossPower = Random.Range(600,300) + spawnedCollectible * Random.Range(15, 50);
    }

    [SerializeField] TextMeshProUGUI levelNowTxt, levelNextTxt;
    [SerializeField] Slider levelSlider;
   
    [SerializeField] GameObject player;

    public void win()
        
    {
        currentLevel++;
        SavePlayer();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void lose()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    int levelNow, levelNext;
    // Update is called once per frame
    void Update()
    {
        levelSlider.value = player.transform.position.z;

        levelNow = currentLevel;
        levelNext = currentLevel + 1;
        levelNowTxt.text = "" + levelNow;
        levelNextTxt.text = "" + levelNext;

     
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            GenerateLevel();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SavePlayer();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoadPlayer();
        }
    }
}
