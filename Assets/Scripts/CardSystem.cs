
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CardSystem : MonoBehaviour
{

    public static CardSystem Instance;

    [SerializeField] RectTransform handContainer;

    [SerializeField] AnimationCurve curve;
    [SerializeField] float curveAmplitude;
    [SerializeField] float widthAmplitude;
    [SerializeField][Range(0, 90)] float maxRotation;

    [SerializeField] GameObject cardPrefab;


    List<Card> hand = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation()
    {
        DrawCard();
        StartCoroutine(WaitDrawCard());
        
        IEnumerator WaitDrawCard()
        {
            yield return new WaitForSeconds(0.5f);
            DrawCard();
            yield return new WaitForSeconds(0.5f);
            DrawCard();
        }

        
    }

    private void DrawCard()
    {
        var card = Instantiate(cardPrefab, handContainer).GetComponent<Card>();
        card.transform.position = GetComponentInParent<Transform>().position;
        hand.Insert(hand.Count / 2, card);

        for (int i = 0; i < hand.Count; i++)
        {
            MoveCard(i, hand[i].transform);
        }
    }

    private void MoveCard(int index, Transform cardTransform)
    {
        var ratio = hand.Count == 1 ? 0.5f : (float)index / (float)(hand.Count - 1);
        var xPos = Mathf.Lerp(handContainer.offsetMax.x, handContainer.offsetMin.x, ratio);
        var yPos = curve.Evaluate(ratio);
        cardTransform.DOLocalMove(new Vector2(xPos * widthAmplitude, yPos * curveAmplitude + handContainer.transform.position.y), 0.2f);
        cardTransform.DORotate(new Vector3 (0,0,Mathf.Lerp(maxRotation, -maxRotation, ratio)), 0.2f);
    }
}
