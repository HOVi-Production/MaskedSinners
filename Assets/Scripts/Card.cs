using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    public CardType type;

    public UnityEvent<Card> OnCardSelected = new();

    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        transform.DOScale(1.2f, 0.1f);
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void OnMouseExit()
    {
        transform.DOScale(1f, 0.1f);
    }

    public void OMouseDown()
    {
        OnCardSelected.Invoke(this);
    }
}

public enum CardType
{
    Mingle, 
    Provoke,
    Accuse
}