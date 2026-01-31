
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

    [SerializeField] NPC currentNPC;


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

    public void StartConversation(NPC npc)
    {
        currentNPC = npc;

        if(currentNPC.CurrentCards.Count != 0)
        {
            DrawCard(currentNPC.CurrentCards[0]);
            for(int i = 1; i < currentNPC.CurrentCards.Count; i++)
            {
                StartCoroutine(WaitDrawCard(i * 0.5f, currentNPC.CurrentCards[i]));
            }
            return;
        }

        DrawCard();
        StartCoroutine(WaitDrawCard(0.5f));
        StartCoroutine(WaitDrawCard(1f));
        
        IEnumerator WaitDrawCard(float wait, CardType? type = null)
        {
            yield return new WaitForSeconds(wait);
            DrawCard(type);
        }

        
    }

    public void ClearHand()
    {
        for(int i = 0; i < hand.Count; i++)
        {
            Destroy(hand[i].gameObject);
        }
        hand.Clear();
    }

    private void DrawCard(CardType? cardType = null)
    {
        var type = cardType ?? currentNPC.GetRandomCard();

        if(type == null)
        {
            return;
        }

        var card = Instantiate(cardPrefab, handContainer).GetComponent<Card>();
        card.transform.position = new Vector3(GetComponentInParent<RectTransform>().position.x, GetComponentInParent<RectTransform>().offsetMin.y, 0);
        card.OnCardSelected.AddListener(OnCardSelected);
        hand.Insert(hand.Count / 2, card);

        RefreshCardPositions();
    }

    private void MoveCard(int index, Transform cardTransform)
    {
        var ratio = hand.Count == 1 ? 0.5f : (float)index / (float)(hand.Count - 1);
        var xPos = Mathf.Lerp(handContainer.offsetMax.x, handContainer.offsetMin.x, ratio);
        var yPos = curve.Evaluate(ratio);
        cardTransform.DOLocalMove(new Vector2(xPos * widthAmplitude, yPos * curveAmplitude + handContainer.transform.position.y), 0.2f);
        cardTransform.DORotate(new Vector3 (0,0,Mathf.Lerp(maxRotation, -maxRotation, ratio)), 0.2f);
    }

    private void RefreshCardPositions()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            MoveCard(i, hand[i].transform);
        }
    }

    private void OnCardSelected(Card card)
    {
        hand.Remove(card);
        RefreshCardPositions();

        card.transform.DORotate(Vector3.zero, 0.1f);

        var sequence = DOTween.Sequence();
        sequence.Append(card.transform.DOMove(GetComponentInParent<Transform>().position, 0.2f));
        sequence.AppendInterval(0.5f);
        sequence.Append(card.transform.DOScale(2f, 0.1f));
        sequence.OnComplete(() =>
        {
            currentNPC.OnCardSelected(card.type);
            Destroy(card.gameObject);
            DrawCard();
        });
        sequence.Play();
    }


}
