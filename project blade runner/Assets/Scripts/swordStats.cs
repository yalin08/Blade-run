using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class swordStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI power;
    public float speed;
    public float damage;
    public float durabilityNow;
    public float durabilityMax;
    float durabilitySlider;
   public Slider slider;
    PlayerMoveScript playerMoveScript;
  public  swipeMechanic swipeMechanic;
    private void Start()
    {
        playerMoveScript = GetComponent<PlayerMoveScript>();
        durabilityNow = durabilityMax;
        durabilitySlider = durabilityNow;
    }

    private void Update()
    {
        if (durabilityNow > durabilityMax)
        {
            durabilityNow = durabilityMax;
        }
        power.text = ""+damage;
        durabilitySlider = Mathf.MoveTowards(durabilitySlider, durabilityNow,Time.deltaTime*5);

        if (!playerMoveScript.isStopped)
        {
            durabilityNow -= Time.deltaTime;
            
        }
        slider.value = durabilitySlider / durabilityMax;

        if (durabilityNow <= 0)
        {
            Time.timeScale = 0;
            swipeMechanic.lose();
        }
    }

}
