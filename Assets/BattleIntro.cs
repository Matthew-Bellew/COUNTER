using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIntro : MonoBehaviour
{
    public Sprite[] Profiles = new Sprite[3];
    public Sprite[] Names = new Sprite[3];
    SpriteRenderer[] ProfileandNameRends;
    SpriteRenderer NameSprite;

    void Awake()
    {
        //simplest way to get both sprite renderers seems to be to get an array of them. Remember to put profile first.
        ProfileandNameRends = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacter(int Level)
    {
        ProfileandNameRends[0].sprite = Profiles[Level];
        ProfileandNameRends[1].sprite = Names[Level];
    }
}
