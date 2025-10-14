
using TMPro;
using UnityEngine;

public class InteractionWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private CanvasGroup canvasGroup;
    public Camera worldCamera;

    private string inputString = "E";
    private string actionString = "";
    private bool isVisible = false;


    void Start()
    {
        inputText.text = inputString;
        actionText.text = actionString;

        //Auto hide
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        //visibility
        var targetAlpha = isVisible ? 1 : 0;
        var currentAlpha = canvasGroup.alpha;
        var newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, 0.1f);
        canvasGroup.alpha = newAlpha;

        //face camera
        transform.rotation = worldCamera.transform.rotation;
    }

    public void SetInpuiText(string text)
    {
        inputString = text;
        inputText.text = inputString;
    }
    public void SetActionText(string text)
    {
        actionString = text;
        actionText.text = actionString;
    }

    public void Show()
    {
        isVisible = true;
    }

    public void Hide()
    {
        isVisible = false;
    }
}
