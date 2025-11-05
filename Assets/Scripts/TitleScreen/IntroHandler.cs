using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroHandler : MonoBehaviour
{
    //screen elements to mute
    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject newGameButton;

    //intro elements
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] private GameObject pressMessage;
    public float typingSpeed = 0.05f;
    public GameObject screenButton;

    //fade out elements
    public float fadeOutDuration;
    private bool isFadingOut;
    public Animator thisAnimator;

    //intro text
    private string intro =
    "Dentro dos circuitos de um vasto programa de computador, tudo tem um propósito. Cada objeto, cada criatura, cada existência é uma variável com valor, tipo e função. \nNeste universo digital vive <b>Lun</b> — um pequeno robô de memória com uma habilidade rara: ele pode assumir qualquer forma entre os três tipos primitivos — <color=#052D9C>Inteiro</color>, <color=#C53333>Caractere</color> e <color=#55FF56>Booleano</color>. Com sua fiel <b>Ferramenta de Atribuição</b>, Lun mantém a ordem do sistema, copiando valores, ajustando variáveis e garantindo que tudo funcione como previsto. \nMas hoje, algo está errado. \nDurante um ciclo de processador rotineiro, uma explosão ecoa pelos barramentos. Blocos corrompidos, variáveis instáveis, funções sem retorno... O programa está em colapso. \nAgora, cabe a Lun investigar o que causou essa falha e restaurar a integridade do sistema. \nPrepare-se para entrar em um mundo onde lógica é vida, e cada bit conta. \nA depuração começa agora...";

    

    void Start()
    {
        pressMessage.SetActive(false);
        screenButton.SetActive(false);
    }
    public void StartIntro()
    {
        gameObject.SetActive(true);
        scoreScreen.SetActive(false);
        title.SetActive(false);
        newGameButton.SetActive(false);
        StartCoroutine(TypeIntroText());
        StartCoroutine(ShowPressToContinueMessage());

    }

    public void FadeOut()
    {
        if (isFadingOut) return;
        isFadingOut = true;

        //Begin animator
        thisAnimator.enabled = true;

        //schedule scene
        StartCoroutine(TransitionToNextScene());
    }

    private IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(fadeOutDuration);
        SceneManager.LoadScene("Scenes/SceneOne");
    }

    private IEnumerator TypeIntroText()
    {
        introText.text = intro;
        introText.ForceMeshUpdate();
        int contentLength = introText.textInfo.characterCount;
        for (int i = 0; i <= contentLength; i++)
        {
            introText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
        private IEnumerator ShowPressToContinueMessage()
    {
        //coroutine to show message to close tutorial window
        yield return new WaitForSeconds(3);
        pressMessage.SetActive(true);
        screenButton.SetActive(true);
    }
}
