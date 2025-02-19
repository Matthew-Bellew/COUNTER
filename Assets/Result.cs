using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    SpriteRenderer ActiveSprite;
    public Sprite GameOverSprite;
    public Sprite WinMatchSprite;
    public Sprite WinGameSprite;

    void Awake()
    {
        ActiveSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void GameOver()
    {
        ActiveSprite.sprite = GameOverSprite;
    }

    public void WinMatch()
    {
        ActiveSprite.sprite = WinMatchSprite;
    }

    public void WinGame()
    {
        ActiveSprite.sprite = WinGameSprite;
    }

}
