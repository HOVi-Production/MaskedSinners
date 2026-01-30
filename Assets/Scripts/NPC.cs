using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera;
    [SerializeField] private Transform playerConversationPos;
    [SerializeField] private GameObject dialogContainer;
    [SerializeField] private Writer writer;
    public Writer Writer => writer;

    [SerializeField] private Challenge startChallenge;
    [SerializeField] private List<Challenge> challenges;
    [SerializeField] private List<CardType> allResponses = new();

    private List<CardType> remainingResponses = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainingResponses.AddRange(allResponses);
        dialogContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversation()
    {
        camera.Priority = 2;

        Player.Instance.transform.DOMove(playerConversationPos.position, 0.2f);
        Player.Instance.PlayerMovement.CanMove = false;

        dialogContainer.SetActive(true);

        if(!startChallenge.asked)
        {
            writer.Write(startChallenge.Line);
            startChallenge.asked = true;
        }
        else
        {
            writer.Write(challenges.First(c => !c.asked).Line);
        }

        writer.OnWritingDone.AddListener(OnWritingDone);
    }

    private void OnWritingDone()
    {
        writer.OnWritingDone.RemoveListener(OnWritingDone);
        CardSystem.Instance.StartConversation(this);
    }

    public CardType? GetRandomCard()
    {
        if(remainingResponses.Count == 0)
        {
            return null;
        }

        var index = UnityEngine.Random.Range(0, remainingResponses.Count);
        var card = remainingResponses[index];
        remainingResponses.RemoveAt(index);
        return card;
    }

    public void OnCardSelected(CardType type)
    {
        
    }
}

[Serializable]
public struct Challenge
{
    public string Line;
    public CardType correctResponse;
    public string correctResponseText;
    public string incorrectResponseText;
    [HideInInspector]
    public bool asked;
    [HideInInspector]
    public bool passed;
}