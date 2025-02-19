using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTimer : MonoBehaviour
{
    public float timeRemaining = 10;
    //public float PlayTime = 0;
    public bool timerIsRunning = false;
    BattleManager BattleM;
    PlayerBattle Player;

    void Awake()
    {
        BattleM = FindObjectOfType<BattleManager>();
        Player = FindObjectOfType<PlayerBattle>();
    }

    void Update()
    {
        //PlayTime += Time.deltaTime;
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                //DisplayCountdown();
            }
            else
            {
                Debug.Log("Player cooldown time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                Player.AttackOn();
            }
        }
    }

    public void SetTimer()
    {
        timeRemaining = Player.CooldownTime;
    }

    public void TimerStart()
    {
        timerIsRunning = true;
    }

    public void TimerStop()
    {
        timerIsRunning = false;
    }

}

