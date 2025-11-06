
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance { get; private set; }

    //game states
    [HideInInspector] public bool isGameOver;
    [HideInInspector] public bool isGameWon;

    //Interaction
    public List<Interaction> interactionList;
    public GameObject player;

    //score
    [Header("Score")]
    private ScoreManager sm;
    private int score;
    private static readonly int scoreFactor = 100;
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
    public AudioSource gameplayMusic;
    public AudioSource bossMusic;

    //Dialog database
    [Header("Diálogo")]
    public DialogDatabase dialogDatabase;
    public AudioClip dialogSound;


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

        //initialize score manager
        sm = ScoreManager.Instance;
    }
    void Start()
    {

        //play music
        var musicTargetVolume = gameplayMusic.volume;
        gameplayMusic.volume = 0;
        gameplayMusic.Play();
        StartCoroutine(FadeAudioSource.StartFade(gameplayMusic, 1f, musicTargetVolume));

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
        if (player.transform.position.y < -20)
        {
            player.transform.position = new Vector3(70,-12,66);
        }
    }

    public void IncreaseScore()
    {
        score++;
        gameplayUI.SetScoreText();
        audioSource2D.PlayOneShot(addScoreSound, 0.7f);
    }
    public void DecreaseScore()
    {
        score--;
        if (score < 0) score = 0;
        gameplayUI.SetScoreText();
        audioSource2D.PlayOneShot(loseScoreSound, 0.7f);
    }

    public string PrintScore()
    {
        return (score * scoreFactor).ToString();
    }

    public void Endgame()
    {
        var finalScore = score * scoreFactor;
        isGameOver = true;
        gameplayUI.SetEndGameScore();
        gameplayUI.ShowGameOverScreen();
        gameplayUI.SetEndGamePlayerName();
        if (sm.IsNewRecord(finalScore))
        {
            gameplayUI.ShowNewRecord(true);
            sm.SaveNewRecord(finalScore);
        }
        else
        {
            gameplayUI.ShowNewRecord(false);
        }

    }
    
    public void StartBossBattle()
    {
        //stop gameplay music
        StartCoroutine(FadeAudioSource.StartFade(gameplayMusic, 2f, 0));

        //play boss music
        var bossMusicVolume = bossMusic.volume;
        bossMusic.volume = 0;
        bossMusic.Play();
        StartCoroutine(FadeAudioSource.StartFade(
            bossMusic,
            0.5f,
            bossMusicVolume
        ));
    }



}
