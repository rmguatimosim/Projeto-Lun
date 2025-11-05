using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    //components
    private ScoreManager sm;


    //score screen info
    [Header("Tela de pontuação")]
    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private TextMeshProUGUI hiScorePlayer;
    [SerializeField] private TextMeshProUGUI hiScoreValue;
    [SerializeField] private TMP_InputField playerName;

    //intro screen
    [SerializeField] private IntroHandler introScene;

    void Awake()
    {
        sm = ScoreManager.Instance;
    }
    void Start()
    {
        scoreScreen.SetActive(false);
        //introScene.SetActive(false);
    }
    public void ShowProfileScreen()
    {
        scoreScreen.SetActive(true);
        hiScorePlayer.text = sm.PrintHighScorePlayer();
        hiScoreValue.text = sm.PrintHighScore();

    }

    public void ShowIntro()
    {
        var name = playerName.text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            name = "Jogador";
        }
        sm.SetPlayerName(name);
        introScene.StartIntro();
        
    }


}
