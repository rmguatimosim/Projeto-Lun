
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

    //Checkpoint
    public static Vector3 lastCheckpointPosition;
    public static bool checkpointReached = false;

    //Rendering
    [Header("Rendering")]
    public Camera worldUiCamera;

    //Physics
    [Header("Physics")]
    public LayerMask groundLayer;

    //UI
    [Header("UI")]
    public GameplayUI gameplayUI;

    // //music
    // [Header("Music")]
    // public AudioSource gameplayMusic;
    // public AudioSource bossMusic;
    // public AudioSource ambienceMusic;


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

        public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        checkpointReached = true;
    }

}
