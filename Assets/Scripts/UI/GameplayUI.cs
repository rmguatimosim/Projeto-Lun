
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{

    [Header("Objects")]
    private PlayerController pc;
    private GameManager gm;

    //initial animation
    [Header("Animações de UI")]
    public float wakeUpTime = 3f;
    [SerializeField] private Canvas thisCanvas;
    public Animator thisAnimator;
    public GameObject initialTutorialTrigger;


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
    public Image tutorialImage;
    private TutorialMessage tutorialMessagePT;
    private int tutorialIndex;
    private bool hasPressedKey;
    private bool canPressToClose;
    private bool hasNextPage;
    private TutorialTrigger nextPage;
    public List<Sprite> tutorialImages;


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

    [Header("Diálogo")]
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TextMeshProUGUI dialogContent;
    private readonly float typingSpeed = 0.05f;
    private Coroutine closeCoroutine;
    private Coroutine typingCoroutine;





    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI endGameScoreText;


    void Awake()
    {
        gm = GameManager.Instance;
        sm = ScoreManager.Instance;
        pc = gm.player.GetComponent<PlayerController>();
        tutorialMessagePT = new TutorialMessage();
        nextPage = null;
        obTextPT = new ObjectiveText();


    }
    // Start is called before the first frame update
    void Start()
    {
        tutorialIndex = 0;
        gameOverScreen.SetActive(false);
        if(sm != null)
        {
            playerName.text = sm.GetPlayerName();
        }
        StartCoroutine(EnableCanvas());


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

    // form display methods
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

    //energy bar methods
    public void SetEnergyBar()
    {
        energyContainer.sprite = energyIcons[pc.thisHealth.health];
    }

    // location display methods
    public void SetLocationText(string txt)
    {
        location.text = txt;
    }

    //objective display methods
    public void SetObjectiveText(int index)
    {
        objectiveText.text = obTextPT.Msg[index];
    }

    //tutorial methods
    public void ToggleTutorial(TutorialTrigger trigger, int index)
    {
        if (hasPressedKey)
        {
            hasPressedKey = false;
        }
        tutorial.SetActive(true);
        tutorialText.text = tutorialMessagePT.Msg[index];
        tutorialImage.sprite = tutorialImages[index];
        pressMessage.SetActive(false);
        StartCoroutine(ShowPressAnyKeyMessage());
        pc.isInCutscene = true;
        hasNextPage = trigger.hasNextPage;
        nextPage = trigger.nextPage;
        tutorialIndex = index;
    }
    private IEnumerator ShowPressAnyKeyMessage()
    {
        //coroutine to show message to close tutorial window
        yield return new WaitForSeconds(1);
        pressMessage.SetActive(true);
        canPressToClose = true;
    }

    //method that updates the score;
    public void SetScoreText()
    {
        scoreText.text = gm.PrintScore();
    }

    //dialog box methods
    public void SetDialogText(DialogEntry entry)
    {
        dialogBox.SetActive(true);
        
            // Stop previous typing coroutine
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        // Stop previous close coroutine
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
            closeCoroutine = null;
        }

        // Start new typing coroutine
        typingCoroutine = StartCoroutine(TypeDialogText(entry.key));

    }

    private IEnumerator TypeDialogText(string key)
    {
        //create effect of text being typed 
        string text = gm.dialogDatabase.GetDialog(key);
        dialogContent.text = text;
        dialogContent.ForceMeshUpdate();
        int contentLength = dialogContent.textInfo.characterCount;
        for (int i = 0; i<=contentLength; i++ )
        {
            dialogContent.maxVisibleCharacters = i;
            if (gm.dialogSound != null && gm.audioSource2D != null)
            {
                gm.audioSource2D.PlayOneShot(gm.dialogSound, 0.5f);
            }

            yield return new WaitForSeconds(typingSpeed);
        }
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }

        // Start new close coroutine
        closeCoroutine = StartCoroutine(DialogBoxClose(gm.dialogDatabase.CalculateTimeToClose(key)));

    }

    private IEnumerator DialogBoxClose(float closeTimer)
    {
        //timer after which the dialog window will close automatically
        yield return new WaitForSeconds(closeTimer);
        dialogBox.SetActive(false);
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

    // fade in at the start
    private IEnumerator EnableCanvas()
    {
        thisAnimator.enabled = true;
        yield return new WaitForSeconds(wakeUpTime);
        initialTutorialTrigger.SetActive(true);
    }



}
