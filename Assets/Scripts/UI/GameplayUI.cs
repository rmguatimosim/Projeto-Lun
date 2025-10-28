
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds3 = new WaitForSeconds(3);
    [SerializeField] private Canvas thisCanvas;

    [Header("Objects")]
    private PlayerController pc;

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

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;



    void Awake()
    {
        pc = GameManager.Instance.player.GetComponent<PlayerController>();
        tutorialMessagePT = new TutorialMessage();
        nextPage = null;

    }
    // Start is called before the first frame update
    void Start()
    {
        tutorialIndex = 0;
        gameOverScreen.SetActive(false);
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
            tutorialIndex++;
            if (hasNextPage)
            {
                ToggleTutorial(nextPage);
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

    public void ToggleTutorial(TutorialTrigger trigger)
    {
        if (hasPressedKey)
        {
            hasPressedKey = false;
        }
        tutorial.SetActive(true);
        tutorialText.text = tutorialMessagePT.Msg[tutorialIndex];
        pressMessage.SetActive(false);
        StartCoroutine(ShowPressAnyKeyMessage());
        pc.isInCutscene = true;
        hasNextPage = trigger.hasNextPage;
        nextPage = trigger.nextPage;
    }



    public void SetObjectiveText(string txt)
    {
        objectiveText.text = txt;
    }

    //coroutine to show message to close tutorial window
    private IEnumerator ShowPressAnyKeyMessage()
    {
        yield return _waitForSeconds3;
        pressMessage.SetActive(true);
        canPressToClose = true;
    }

    //method that updates the score;
    public void SetScoreText(int score)
    {
        int result = score * 100;
        scoreText.text = result.ToString();
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
        SceneManager.LoadScene("Scenes/TittleScreen");
    }

    private IEnumerator GameOverTimer()
    {
        yield return new WaitForSeconds(2);
        gameOverScreen.SetActive(true);
    }

}
