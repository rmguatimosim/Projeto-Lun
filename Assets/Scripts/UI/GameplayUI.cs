
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Canvas thisCanvas;

    [Header("Objects")]
    private PlayerController pc;
    private GameManager gm;

    //display form and content
    [Header("Forma e conteúdo")]
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI currentType;
    [SerializeField] private TextMeshProUGUI value;

    //tutorial
    [Header("Tutorial")]
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject pressMessage;
    [SerializeField] private TextMeshProUGUI tutorialText;
    private TutorialMessage tutorialMessagePT;
    private int tutorialIndex;
    private bool hasPressedKey;
    private bool canPressToClose;
    private bool hasNextPage;
    private TutorialTrigger nextPage;


    //energy bar
    [Header("Energia")]
    public Image energyContainer;
    public List<Sprite> energyIcons;

    //display  location
    [Header("Localização")]
    [SerializeField] private TextMeshProUGUI location;

    //display objective
    [Header("Objetivo")]
    [SerializeField] private TextMeshProUGUI objectiveText;
    private ObjectiveText obTextPT;

    [Header("Score")]
    private ScoreManager sm;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject newRecord;


    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI endGameScoreText;


    void Awake()
    {
        gm = GameManager.Instance;
        sm = ScoreManager.Instance;
        pc = gm.player.GetComponent<PlayerController>();
        tutorialMessagePT = new TutorialMessage();
        obTextPT = new ObjectiveText();
        nextPage = null;

    }
    // Start is called before the first frame update
    void Start()
    {
        tutorialIndex = 0;
        gameOverScreen.SetActive(false);
        playerName.text = sm.GetPlayerName();

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasPressedKey && pc.anyKeyAction.ReadValue<float>() == 1 && canPressToClose)
        {

            hasPressedKey = true;
            tutorial.SetActive(false);
            canPressToClose = false;
            pc.isInCutscene = false;
            if (hasNextPage)
            {
                ToggleTutorial(nextPage, tutorialIndex+1);
            }
        }

    }

    public void SetTypeText()
    {
        typeText.text = pc.selectedVarForm.varType.ToString();

    }
    public void SetCurrentTypeText()
    {
        currentType.text = pc.currentVarForm.varType.ToString();
    }
    public void SetValueText()
    {
        if (pc.currentVarForm.varType == Player.VarType.Null)
        {
            value.text = "null";
        }
        else if (pc.currentVarForm.varType == Player.VarType.Bool)
        {
            value.text = pc.content == 0 ? "false" : "true";
        }
        else if (pc.currentVarForm.varType == Player.VarType.Char)
        {
            if (pc.content == 0)
            {
                value.text = "' '";
                return;
            }
            char cont = (char)pc.content;
            value.text = "'" + cont.ToString() + "'";
        }
        else
        {
            value.text = pc.content.ToString();
        }
    }
    public void SetEnergyBar()
    {
        energyContainer.sprite = energyIcons[pc.thisHealth.health];
    }

    public void SetLocationText(string txt)
    {
        location.text = txt;
    }

    public void ToggleTutorial(TutorialTrigger trigger, int index)
    {
        if (hasPressedKey)
        {
            hasPressedKey = false;
        }
        tutorial.SetActive(true);
        tutorialText.text = tutorialMessagePT.Msg[index];
        pressMessage.SetActive(false);
        StartCoroutine(ShowPressAnyKeyMessage());
        pc.isInCutscene = true;
        hasNextPage = trigger.hasNextPage;
        nextPage = trigger.nextPage;
        tutorialIndex = index;
    }

    public void SetObjectiveText(int index)
    {
        objectiveText.text = obTextPT.Msg[index];
    }

    //coroutine to show message to close tutorial window
    private IEnumerator ShowPressAnyKeyMessage()
    {
        yield return new WaitForSeconds(1);
        pressMessage.SetActive(true);
        canPressToClose = true;
    }

    //method that updates the score;
    public void SetScoreText()
    {        
        scoreText.text = gm.PrintScore();
    }

    // game over methods
    public void ShowGameOverScreen()
    {
        StartCoroutine(GameOverTimer());
    }

    public void MainMenuButtonPressed()
    {
        StartCoroutine(GoToMainMenu());
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Scenes/TitleScreen");
    }

    private IEnumerator GameOverTimer()
    {
        yield return new WaitForSeconds(2);
        gameOverScreen.SetActive(true);
    }

    public void SetEndGameScore()
    {
        endGameScoreText.text = gm.PrintScore();
    }

    public void ShowNewRecord(bool b)
    {
        newRecord.SetActive(b);
    }



}
