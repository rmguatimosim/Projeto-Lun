
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance { get; private set; }

    [HideInInspector] public bool isGameOver;

    //Interaction
    public List<Interaction> interactionList;
    public GameObject player;

    //score
    [Header("Score")]
    private int score;
    private int highestScore;
    public AudioClip addScoreSound;
    public AudioClip loseScoreSound;

    //camera
    public Camera playerCamera;

    //Physics
    [Header("Physics")]
    public LayerMask groundLayer;

    //UI
    [Header("UI")]
    public GameplayUI gameplayUI;

    //music
    [Header("Music")]
    public AudioSource audioSource2D;
    // public AudioSource bossMusic;




    void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


    }
    void Start()
    {

        // //play music
        // var musicTargetVolume = gameplayMusic.volume;
        // gameplayMusic.volume = 0;
        // gameplayMusic.Play();
        // StartCoroutine(FadeAudioSource.StartFade(gameplayMusic, musicTargetVolume, 1f));

        // //play ambience
        // var ambienceTargetVolume = ambienceMusic.volume;
        // ambienceMusic.volume = 0;
        // ambienceMusic.Play();
        // StartCoroutine(FadeAudioSource.StartFade(ambienceMusic, ambienceTargetVolume, 1f));

        // //listen to OnGameOver
        // GlobalEvents.Instance.OnGameOver += (sender, args) => isGameOver = true;
    }

    void Update()
    {
        FellOffTheMap();
    }

    public Interaction GetNearestInteraction(Vector3 position)
    {
        float closestDst2 = -1;
        Interaction closestInteraction = null;

        foreach (Interaction interaction in interactionList)
        {
            var dst2 = (interaction.transform.position - position).sqrMagnitude;
            var isCacheInvalid = closestDst2 < 0;
            if (isCacheInvalid || dst2 < closestDst2)
            {
                closestDst2 = dst2;
                closestInteraction = interaction;
            }
        }
        return closestInteraction;
    }

    public void FellOffTheMap()
    {
        if (player.transform.position.y < -30)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void IncreaseScore()
    {
        score++;
        gameplayUI.SetScoreText(score);
        audioSource2D.PlayOneShot(addScoreSound, 0.7f);
    }
    public void DecreaseScore()
    {
        score--;
        gameplayUI.SetScoreText(score);
        audioSource2D.PlayOneShot(loseScoreSound, 0.7f);
    }



}
