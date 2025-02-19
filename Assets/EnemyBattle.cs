using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    /*
    public Sprite NeutralSprite;
    public Sprite WindupSprite;
    public Sprite AttackSprite;
    public Sprite BlockSprite;
    public Sprite DefeatSprite;
    public Sprite FakeWindupSprite;
    public Sprite TrapWindupSprite;
    */

    public Sprite[] LJ = new Sprite[5];
    public Sprite[] Ping = new Sprite[6];
    public Sprite[] Ascensor = new Sprite[8];

    public float[] LJWaitTimes = { 1, 3 };
    public float[] PingWaitTimes = { 1, 3 };
    public float[] AscensorWaitTimes = { 1, 3 };

    SpriteRenderer ActiveSprite;
    public Sprite[] Sprites = new Sprite[7];
    Sprite[][] SpriteLibrary = new Sprite[3][];

    public int Health = 1;

    PlayerBattle Player;
    BattleManager BattleM;

    public float ActiveWindupTime;
    public float[] LevelWindupTimes = { 1, 2, 3 };
    public int[] LevelHealths = new int[3];
    public float MinWaitTime;
    public float MaxWaitTime;
    float[][] WaitLibrary = new float[3][];



    public bool WindupStarted = false;
    public bool WindupTrap = false;

    bool BlockActive = false;
    bool FakeoutActive = false;

    public bool Trap = false;

    EnemyTimer Timer;

    void Awake()
    {
        Player = FindObjectOfType<PlayerBattle>();
        BattleM = FindObjectOfType<BattleManager>();
        Timer = FindObjectOfType<EnemyTimer>();

        SpriteLibrary[0] = LJ;
        SpriteLibrary[1] = Ping;
        SpriteLibrary[2] = Ascensor;

        WaitLibrary[0] = LJWaitTimes;
        WaitLibrary[1] = PingWaitTimes;
        WaitLibrary[2] = AscensorWaitTimes;

        ActiveSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Do I want characters to all be handled with one script or with instances?
    public void SetCharacter(int Level)
    {
        Debug.Log("Setting enemy - level is " + Level);
        ActiveWindupTime = LevelWindupTimes[Level];
        Health = LevelHealths[Level];
        Sprites = SpriteLibrary[Level];
        MinWaitTime = WaitLibrary[Level][0];
        MaxWaitTime = WaitLibrary[Level][1];
    }

    public IEnumerator WaitThenRandAttack()
    {
        Debug.Log("The enemy is choosing an attack...");
        yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));

        //put the stuff in here to see which attacks they have and choose one
        if (BattleM.Level == 1 && Random.value >= 0.5)
        {
            StartCoroutine(FakeWindup());
        }
        else if (BattleM.Level == 2 && Random.value >= 0.5)
        {
            TrapWindup();
        }
        else
        {
            Windup();
        }
    }

        public void Windup()
    {
        Debug.Log("Enemy is Winding up");
        WindupAnim();
        WindupStarted = true;
        Timer.TimerStart();
    }

    IEnumerator FakeWindup()
    {
        Debug.Log("It's a fakeout!");
        FakeoutActive = true;
        FakeWindupAnim();
        yield return new WaitForSeconds(ActiveWindupTime);

        //best way I could think to do this - if ping isn't blocked, it goes back to neutral at the end of the winduptime, but if he is, it lets the block anim play out so it can't be immediately cancelled, e.g. if the block happens immediately before winduptime runs out, and the block anim starts the new waitthenrand when it's done
        //if block activates, deactivates, and fakeout hasn't ended yet, the fakeoutactive check stops it doing anything UNLESS it's activated again by a new fakeout. this code is a mess.
        if (BlockActive == false && WindupStarted == false && FakeoutActive == true)
        {
            FakeoutActive = false;
            NeutralAnim();
            StartCoroutine(WaitThenRandAttack());
        }

    }

    void TrapWindup()
    {
        Debug.Log("It's a trap!");
        //Ascensor doesn't have a fakeout so he can use this same bit 
        FakeWindupAnim();
        WindupStarted = true;
        Trap = true;
        Timer.TimerStart();

    }

    public void StopWindup()
    {
        WindupStarted = false;
    }

    public void TakeDmg()
    {
        Health -= 1;
    }

    public void TrapOff()
    {
        Trap = false;
    }




    //Animations start here

    public void NeutralAnim()
    {
        //ActiveSprite.sprite = NeutralSprite;
        ActiveSprite.sprite = Sprites[0];
    }

    public void WindupAnim()
    {
        Debug.Log("Enemy began windup anim");
        //ActiveSprite.sprite = WindupSprite;
        ActiveSprite.sprite = Sprites[1];
    }

    public void AttackAnim()
    {
        //ActiveSprite.sprite = AttackSprite;
        ActiveSprite.sprite = Sprites[2];
    }

    public IEnumerator BlockAnim()
    {
        Debug.Log("Blocked!");
        BlockActive = true;
        //ActiveSprite.sprite = BlockSprite;
        ActiveSprite.sprite = Sprites[3];
        yield return new WaitForSeconds(1);
        if (WindupStarted == false)
        {
            NeutralAnim();
        }

        //couldn't think of a better place to put this - it doesn't need to worry about windupstarted because that can't happen in this scenario where it's still waiting for the waitthenrand
        if (FakeoutActive == true)
        {
            FakeoutActive = false;
            StartCoroutine(WaitThenRandAttack());
        }
        BlockActive = false;
    }

    public void DefeatAnim()
    {
        //ActiveSprite.sprite = DefeatSprite;
        ActiveSprite.sprite = Sprites[4];
    }

    public void FakeWindupAnim()
    {
        //Ascensor doesn't have a fakeout so he can use this same bit 

        //ActiveSprite.sprite = FakeWindupSprite;
        ActiveSprite.sprite = Sprites[5];
    }

    public void TrapRevealAnim()
    {
        ActiveSprite.sprite = Sprites[6];
    }

    public void TrapHitAnim()
    {
        ActiveSprite.sprite = Sprites[7];
    }
}
