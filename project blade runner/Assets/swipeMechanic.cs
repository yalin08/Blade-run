using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]

public class swipeMechanic : MonoBehaviour
{
    [SerializeField] swordStats swordStats;
    [SerializeField] Slider swipeSlider;
    [SerializeField] DynamicJoystick dynamicJoystick;
    [SerializeField] GameObject multiplierText;
    [SerializeField] float pointDivide;
    [SerializeField] float pointMultiplier;
    [SerializeField] GameObject BossGameObject;
    [SerializeField] Animator swordAnim;

    [SerializeField] levelManager levelManager;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;
    TextMeshProUGUI TextMeshProUGUI;
    string text;
    public float points;
    float bossPower;
    bossStatsScript bossStatsScript;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI = multiplierText.GetComponent<TextMeshProUGUI>();
        InvokeRepeating("decreasePoint", 1f, 0.2f);
        points = 1;
        zrot = multiplierText.transform.localEulerAngles.z;
        timer = Timer;
        bossStatsScript = BossGameObject.GetComponent<bossStatsScript>();
        bossPower = bossStatsScript.bossPower;
    }
   

    public bool didWin=false;
  
    Vector2 vectorFirst;
     Vector2 vectorSecond;
    void onSwipeMinigameEnd()
    {
        timer = Timer;

        swordStats.damage *= pointMultiplier;
        swipeMinigameStarted = false;
        points = 1;
        BossGameObject.GetComponentInChildren<bossHeadScript>().enabled = false;

        swordAnim.enabled = true;
        swordAnim.SetBool("start", true);
        swordAnim.SetBool("Boss",true);
        if (swordStats.damage>bossPower)
        {
            swordAnim.SetBool("BossWin", true);
            Debug.Log("win");
            didWin = true;
            Invoke("win",1f);


        }
        else
        {
            swordAnim.SetBool("BossLose", true);
            Debug.Log("lose");
            didWin = false;

            Invoke("lose", 1f);

        }
        gameObject.SetActive(false);
    }
    float f;
    public bool swipeMinigameStarted=false;

   public void win()
    {
        winUI.SetActive(true);
    }

   public void lose()
    {

        loseUI.SetActive(true);
    }
    public float Timer;
    float timer;
    // Update is called once per frame
    void Update()
    {
        if (swipeMinigameStarted)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                onSwipeMinigameEnd();
                
            }
        }


        textSize();
        vectorFirst = dynamicJoystick.diff;
        if (vectorFirst!= vectorSecond)
        {
            swipeMinigameStarted = true;
            points+= Mathf.Abs(f);
        }
        swipeSlider.value = points / pointDivide;
        if (points / pointDivide > swipeSlider.maxValue)
        {
            swipeSlider.maxValue = points / pointDivide;
        }
        pointMultiplier= Mathf.Ceil(points / pointDivide);
        TextMeshProUGUI.text = "x"+pointMultiplier+" Points!";
        //   f = Mathf.Abs(dynamicJoystick.Horizontal) +Mathf.Abs(dynamicJoystick.Vertical);

        if (Mathf.Abs(dynamicJoystick.Horizontal) > Mathf.Abs(dynamicJoystick.Vertical))
        {
            f = Mathf.Abs(dynamicJoystick.Horizontal);
        }
        else
            f = Mathf.Abs(dynamicJoystick.Vertical);

    }
    public float zrot;
    Color color= new Color(15,0,0);
    void textSize()
    {
        multiplierText.transform.localScale = Vector2.one*(pointMultiplier/3.5f+2.3f);
        Quaternion quaternion = Quaternion.Euler(0,0,zrot);

        color.g = 1-pointMultiplier / 10;color.b = 1-pointMultiplier / 10;
       TextMeshProUGUI.color = color;

        multiplierText.transform.rotation = Quaternion.RotateTowards(multiplierText.transform.rotation, quaternion, 7*pointMultiplier * Time.deltaTime);
        if (quaternion == multiplierText.transform.rotation)
        {

            zrot = -zrot;
        }

    }
    void decreasePoint()
    {
        if (points-1 > 1)
        {
            points-=1;
        }
        
        
    }
    private void LateUpdate()
    {
        
      
        vectorSecond = dynamicJoystick.diff;
    }

}
