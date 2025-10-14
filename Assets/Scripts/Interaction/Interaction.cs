
using System;
using EventArgs;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{

    public float radius = 2f;

    private bool isAvailable = true;
    private bool isActive;

    public event EventHandler<InteractionEventArgs> OnInteraction;

    void OnEnable()
    {
        GameManager.Instance.interactionList.Add(this);
    }

    void OnDisable()
    {
        GameManager.Instance.interactionList.Remove(this);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SetActive(bool isActive)
    {
        var wasActive = this.isActive;
        this.isActive = isActive;

        // //Update interactionWidget
        // var interactionWidget = widget.GetComponent<InteractionWidget>();
        // if (isActive)
        // {
        //     interactionWidget.Show();
        // }
        // else
        // {
        //     interactionWidget.Hide();
        // }
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void SetAvailable(bool isAvailable)
    {
        this.isAvailable = isAvailable;
    }

    public void Interact(InteractionType action)
    {
        //Invoke event
        OnInteraction?.Invoke(this, new InteractionEventArgs
        {
            action = action
        });
    }

    // public void SetActionText(string text)
    // {
    //     // var interactionWidget = widget.GetComponent<InteractionWidget>();
    //     // interactionWidget.SetActionText(text);

    // }
}
