using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public int Level = 0;
    public int NumberOfMatches = 2;
    PlayerBattle Player;
    EnemyBattle Enemy;
    PlayerTimer PTimer;
    EnemyTimer ETimer;
    Background BG;
    GameObject Battleintro;
    Result MatchResult;

    GameObject PlayerObject;
    GameObject EnemyObject;

    public AudioSource Audio;
    public AudioClip IntroSound;
    public AudioClip PunchSound;
    public AudioClip TrapSound;
    public AudioClip TrapRevealSound;
    //blocksound is done by player
    public AudioClip LoseSound;
    public AudioClip WinMatchSound;
    public AudioClip WinGameSound;


    void Awake()
    {
        Player = FindObjectOfType<PlayerBattle>();
        Enemy = FindObjectOfType<EnemyBattle>();
        PTimer = FindObjectOfType<PlayerTimer>();
        ETimer = FindObjectOfType<EnemyTimer>();
        BG = FindObjectOfType<Background>();
        MatchResult = FindObjectOfType<Result>();
        Audio = gameObject.GetComponent<AudioSource>();

        Battleintro = GameObject.FindGameObjectWithTag("BattleIntro");

        //I don't know why it won't activate the characters, but I tried calling the objects directly in case it just couldn't reach the script while deactive. It didn't work.
        PlayerObject = Player.gameObject;
        EnemyObject = Enemy.gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        DeactivateCharacters();
        StartCoroutine(MatchStart());
        Battleintro.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MatchStart()
    {
        Debug.Log("Match start");



        if (Level > NumberOfMatches)
        {
            Debug.Log("PLAYER WINS THE GAME!");
            StartCoroutine(WinGame());
        }
        else
        {
            MatchResult.gameObject.SetActive(false);
            Battleintro.SetActive(true);
            Audio.PlayOneShot(IntroSound, 1F);
            Battleintro.GetComponent<BattleIntro>().SetCharacter(Level);
            yield return new WaitForSeconds(2);
            ActivateCharacters();
            //Do I want to reset health each match, or is that too easy?
            Player.ResetHealth();
            Enemy.SetCharacter(Level);
            BG.SetBG(Level);

            //will it deactivate it
            Battleintro.SetActive(false);

            RoundStart();
        }
    }

    void RoundStart()
    {
        Debug.Log("Round start");
        if (Enemy.Health == 0)
        {
            StartCoroutine(WinMatch());
        }
        else if (Player.Health == 0)
        {
            StartCoroutine(LoseMatch());
        }
        else
        {
            UnfreezePlayer();
            Player.NeutralAnim();
            Enemy.StopWindup();
            Enemy.NeutralAnim();
            StartCoroutine(Enemy.WaitThenRandAttack());
        }
    }

    public void PlayerHurt()
    {
        StopTimers();
        FreezePlayer();
        Audio.PlayOneShot(PunchSound, 1F);
        Player.DefeatAnim();
        Enemy.AttackAnim();
        Player.TakeDmg();
        StartCoroutine(NewRound());
    }

    public void EnemyHurt()
    {
        StopTimers();
        FreezePlayer();
        Audio.PlayOneShot(PunchSound, 1F);
        Player.AttackAnim();
        Enemy.DefeatAnim();
        Enemy.TakeDmg();
        StartCoroutine(NewRound());
    }

    public void LOLUGOTBAITED()
    {
        Debug.Log("ahahahaha you fucking MORON");
        StopTimers();
        FreezePlayer();
        Audio.PlayOneShot(PunchSound, 1F);
        Audio.PlayOneShot(TrapSound, 1F);
        Player.AttackAnim();
        Enemy.TrapHitAnim();
        Enemy.TrapOff();
        Player.TakeDmg();
        StartCoroutine(NewRound());
    }

    public IEnumerator TrapReveal()
    {
        Debug.Log("Awww that's sweet");
        FreezePlayer();
        Player.NeutralAnim();
        Audio.PlayOneShot(TrapRevealSound, 1F);
        Enemy.TrapRevealAnim();
        yield return new WaitForSeconds(1);
        UnfreezePlayer();
        Enemy.TrapOff();
        Enemy.NeutralAnim();
        Enemy.StopWindup();
        StartCoroutine(Enemy.WaitThenRandAttack());
    }


    void FreezePlayer()
    {
        Player.StopAnimChanges();
        Player.AttackOff();
    }

    void UnfreezePlayer()
    {
        Player.StartAnimChanges();
        Player.AttackOn();
    }

    public IEnumerator NewRound()
    {
        yield return new WaitForSeconds(1);
        RoundStart();
    }

        void StopTimers()
    {
        PTimer.TimerStop();
        ETimer.TimerStop();
    }

    IEnumerator WinMatch()
    {
        DeactivateCharacters();
        Audio.PlayOneShot(WinMatchSound, 1F);
        MatchResult.gameObject.SetActive(true);
        MatchResult.WinMatch();
        yield return new WaitForSeconds(2);
        Level += 1;
        StartCoroutine(MatchStart());
    }

    IEnumerator LoseMatch()
    {
        DeactivateCharacters();
        Audio.PlayOneShot(LoseSound, 1F);
        MatchResult.gameObject.SetActive(true);
        MatchResult.GameOver();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator WinGame()
    {
        //doesn't need to be turned on because it didn't turn off after winmatch
        MatchResult.WinGame();
        Audio.PlayOneShot(WinGameSound,1F);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Menu");
    }

    void DeactivateCharacters()
    {
        EnemyObject.SetActive(false);
        PlayerObject.SetActive(false);
    }

    void ActivateCharacters()
    {
        Debug.Log("Activating characters");
        EnemyObject.SetActive(true);
        PlayerObject.SetActive(true);
    }

}
