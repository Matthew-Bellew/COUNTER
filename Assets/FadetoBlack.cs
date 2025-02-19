using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadetoBlack : MonoBehaviour
{

    AudioSource Audio;
    public AudioClip StartSound;
    public AudioClip GoSound;
    public string NextScene;
    public bool IsMenu = true;
    bool SpacePressed = false;
    //GameObject FadeScreen;
    Animator Anim;


    void Awake()
    {
        //FadeScreen = GameObject.FindGameObjectWithTag("Fade");
        Audio = gameObject.GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        Debug.Log("Game Started");
    }
    void Start()
    {
        if (IsMenu == true)
        {
            Audio.PlayOneShot(StartSound, 1F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SpacePressed == false && IsMenu == true)
        {
            Debug.Log("Space Pressed");
            SpacePressed = true;
            Audio.PlayOneShot(GoSound, 1F);
            FadeToBlack();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void FadeToBlack()
    {
        Anim.Play("FadetoBlackAnim", 0, 0);
    }

    public void ExitScene()
    {
        SceneManager.LoadScene(NextScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
