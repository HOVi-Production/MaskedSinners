using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract = new();

    [SerializeField] private GameObject _interactIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag != "Player")
        {
            return;
        }
        
        _interactIndicator.SetActive(true);
        InteractController.Instance.SetActiveInteractable(this);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag != "Player")
        {
            return;
        }
        
        _interactIndicator.SetActive(false);
        InteractController.Instance.SetActiveInteractable(null);
    }

    public void Interact()
    {
        OnInteract.Invoke();
        _interactIndicator.SetActive(false);
    }
}
