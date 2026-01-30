using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        transform.DOScale(1.2f, 0.1f);
    }

    public void OnMouseExit()
    {
        transform.DOScale(1f, 0.1f);
    }
}
