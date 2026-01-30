using UnityEngine;

public class InteractController : MonoBehaviour
{
    public static InteractController Instance;
    
    private Interactable currentInteractable;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable?.Interact();
        }
    }

    public void SetActiveInteractable(Interactable interactable)
    {
        currentInteractable = interactable;
    }
}
