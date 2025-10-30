using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    //components
    private ScoreManager sm;
    public float fadeOutDuration = 3f;

    //score screen info
    [Header("Tela de pontuação")]
    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private TextMeshProUGUI hiScorePlayer;
    [SerializeField] private TextMeshProUGUI hiScoreValue;
    [SerializeField]private TMP_InputField playerName;

    void Awake()
    {
        sm = ScoreManager.Instance;
    }
    void Start()
    {
        scoreScreen.SetActive(false);
    }
    public void ShowProfileScreen()
    {
        scoreScreen.SetActive(true);
        hiScorePlayer.text = sm.PrintHighScorePlayer();
        hiScoreValue.text = sm.PrintHighScore();

    }
    public void FadeOut()
    {
        var name = playerName.text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            name = "Jogador";
        }
        sm.SetPlayerName(name);
        StartCoroutine(TransitionToNextScene());
    }


    private IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(fadeOutDuration);
        SceneManager.LoadScene("Scenes/SceneOne");
    }
}
