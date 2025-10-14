
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    private readonly float scanInterval = 0.5f;
    private float scanCooldown = 0;
    private Interaction currentInteraction;
    private PlayerController pc;

    void Awake()
    {
        pc = gameObject.GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((scanCooldown -= Time.deltaTime) <= 0f)
        {
            scanCooldown = scanInterval;
            ScanObjects();
        }

        //process input
        if (pc.hasCopyInput)
        {
            if (currentInteraction != null)
            {
                currentInteraction.Interact(InteractionType.copy);
                ScanObjects();
            }
        }
        if (pc.hasStoreInput)
        {
            if (currentInteraction != null)
            {
                currentInteraction.Interact(InteractionType.store);
                ScanObjects();
            }    
        }
    }

    private void ScanObjects()
    {
        Interaction nearestInteraction = GetNearestInteraction(transform.position);
        if (nearestInteraction != currentInteraction)
        {
            currentInteraction?.SetActive(false);
            nearestInteraction?.SetActive(true);
            currentInteraction = nearestInteraction;
        }
    }
    
    public Interaction GetNearestInteraction(Vector3 position)
    {
        float closestDst = -1 ;
        Interaction closestInteraction = null;

        //Iterate through objects
        var interactionList = GameManager.Instance.interactionList;
        foreach (Interaction interaction in interactionList)
        {
            var dst = (interaction.transform.position - position).magnitude;
            var isAvailable = interaction.IsAvailable();
            var isCloseEnough = dst <= interaction.radius ;
            var isCacheInvalid = closestDst < 0;
            if (isCloseEnough && isAvailable)
            {
                if (isCacheInvalid || dst < closestDst)
                {
                    closestDst = dst;
                    closestInteraction = interaction;
                }
            }            
        }
        return closestInteraction;
    }
}
