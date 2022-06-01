using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
   public float speed;
    public VariableJoystick variableJoystick;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject button;
    float verticalSpeed;
    float horizontalSpeed;
    swordStats swordStats;


    public void Start()
    {
        swordStats = GameObject.FindWithTag("Player").GetComponent<swordStats>();
    }

    public bool isStopped=true;
    bool gameStarted = false;
    public void startGame()
    {
        if (!gameStarted)
        {
            isStopped = false;
            gameStarted = true;
            Destroy(button);
        }
    }

    public void Update()
    {
        if (!isStopped)
        {
        speed = swordStats.speed;
       
        }
        else
        {
            speed = 0;
        }
        verticalSpeed = speed / 1.2f;
        horizontalSpeed = speed;
      
        
        //transform.Translate(-verticalSpeed * Time.deltaTime,0,variableJoystick.Horizontal * horizontalSpeed * Time.deltaTime);

        Vector3 playerToGo = new Vector3(
            Mathf.Clamp(
                
                transform.position.x + verticalSpeed * variableJoystick.Horizontal*Time.deltaTime , -1.711097f, 1.711097f)
            
            ,transform.position.y,transform.position.z+verticalSpeed*Time.deltaTime


            );

        transform.position = Vector3.MoveTowards(transform.position,playerToGo,speed);
        
        Quaternion targetrot= Quaternion.Euler(variableJoystick.Horizontal*-1, variableJoystick.Horizontal*3, speed/2);

        sword.transform.localRotation = Quaternion.Lerp(sword.transform.localRotation, targetrot, speed*Time.deltaTime*3);
       
    }
}