using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyTimer : MonoBehaviour
{
    public float timeRemaining = 10;
    //public float PlayTime = 0;
    public bool timerIsRunning = false;
    BattleManager BattleM;
    EnemyBattle Enemy;

    void Awake()
    {
        BattleM = FindObjectOfType<BattleManager>();
        Enemy = FindObjectOfType<EnemyBattle>();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                //counts down
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Enemy timer has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                //checks if it's a trap
                if (Enemy.Trap == false)
                {
                    //normal timer, player gets hit
                    BattleM.PlayerHurt();
                }
                else
                {
                    //trap timer, trap is revealed
                    StartCoroutine(BattleM.TrapReveal());
                }
            }
        }
    }

    public void TimerStart()
    {
        timeRemaining = Enemy.ActiveWindupTime;
        timerIsRunning = true;

    }

    /*
    public void TimerWaitStart()
    {
        timeRemaining = Enemy.WaitTime;
        Attacking = false;
        timerIsRunning = true;
    }
    */

    public void TimerStop()
    {
        timerIsRunning = false;
    }


}
