using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] Sprites = new Sprite[3];
    SpriteRenderer ActiveSprite;

    void Awake()
    {
        ActiveSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetBG(int Level)
    {
        ActiveSprite.sprite = Sprites[Level];
    }
}
