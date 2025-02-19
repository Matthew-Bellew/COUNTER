using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public int Health = 1;
    int StartHealth;
    bool AttackReady = true;
    SpriteRenderer ActiveSprite;
    public Sprite NeutralSprite;
    public Sprite AttackSprite;
    public Sprite DefeatSprite;
    public float CooldownTime = 1;
    PlayerTimer Timer;
    EnemyBattle Enemy;
    bool AnimAllowedtoChange = true;
    BattleManager BattleM;
    AudioSource BlockAudio;

    void Awake()
    {
        StartHealth = Health;
        ActiveSprite = gameObject.GetComponent<SpriteRenderer>();
        Timer = FindObjectOfType<PlayerTimer>();
        Enemy = FindObjectOfType<EnemyBattle>();
        BattleM = FindObjectOfType<BattleManager>();
        BlockAudio = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && AttackReady==true)
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Attack!");
        StartCoroutine(AttackAnim());
        if (Enemy.WindupStarted == true)
        {
            if (Enemy.Trap == true)
            {
                BattleM.LOLUGOTBAITED();
            }
            else
            {
                BattleM.EnemyHurt();
            }
        }
        else
        {
            //Enemy blocks attack
            BlockAudio.Play();
            StartCoroutine(Enemy.BlockAnim());
            AttackCooldown();
        }

    }

    public void ResetHealth()
    {
        Health = StartHealth;
    }

    void AttackCooldown()
    {
        AttackOff();
        Timer.SetTimer();
        Timer.TimerStart();
    }


    public void AttackOn()
    {
        Debug.Log("Attack on");
        AttackReady = true;
    }

    public void AttackOff()
    {
        Debug.Log("Attack off");
        AttackReady = false;
    }

    public void TakeDmg()
    {
        Health -= 1;
    }




    public void NeutralAnim()
    {
        ActiveSprite.sprite = NeutralSprite;
    }

    public IEnumerator AttackAnim()
    {
        ActiveSprite.sprite = AttackSprite;
        yield return new WaitForSeconds(1);
        if (AnimAllowedtoChange == true)
        {
            NeutralAnim();
        }
    }

    public void DefeatAnim()
    {
        ActiveSprite.sprite = DefeatSprite;
    }

    public void StopAnimChanges()
    {
        AnimAllowedtoChange = false;
    }

    public void StartAnimChanges()
    {
        AnimAllowedtoChange = true;
    }

}
