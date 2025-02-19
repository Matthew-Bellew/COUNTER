using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBG : MonoBehaviour
{
    Vector3 startPos;
    //loopwidth should be the width of 1 image divided by 100
    public float LoopWidth;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x - LoopWidth)
        {
            transform.position = startPos;
        }
    }
}
