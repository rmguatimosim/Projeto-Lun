using System.Collections;
using Player;
using TMPro;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    //data
    public int expectedData;
    private Data data;
    public VarForm expectedVarForm;
    [HideInInspector] public bool wasActivated = false;
    
    //display
    public TextMeshProUGUI heightText;
    public GameObject body;
    public float elapsed = 0.5f;
    private bool isMoving = false;

    void Awake()
    {
        data = gameObject.GetComponent<Data>();    }

    void Start()
    {
        data.canReceive = !wasActivated;
        heightText.text = "ALTURA\n"+(data.content/10)+"px";
    }

    
    public void ChangeHeight()
    {
        
        heightText.text = "ALTURA\n" + (data.content / 10f) + "px";
        float y = data.content / 10f;
        Vector3 newPosition = new(gameObject.transform.position.x, y - 4f, gameObject.transform.position.z);
        if (!isMoving)
        {
            StartCoroutine(MoveToPosition(newPosition, elapsed));
        }

        if(!wasActivated && data.content == expectedData)
        {
            data.canReceive = false;
            wasActivated = true;
            StartCoroutine(DeleteObstacle());
        }
    }

    public IEnumerator DeleteObstacle()
    {
        yield return new WaitForSeconds(3);
        Destroy(this);
    }

    //coroutine to move the obstacle
    public IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        isMoving = true;
        Vector3 startPosition = body.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            body.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        body.transform.position = targetPosition;
        isMoving = false;
    }
}
