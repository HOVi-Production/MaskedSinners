using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera;
    [SerializeField] private Transform playerConversationPos;
    [SerializeField] private GameObject dialogContainer;
    [SerializeField] private Writer writer;
    public Writer Writer => writer;

    [SerializeField] int correctAnswersRequired;
    [SerializeField] string challengesPassedText;
    [SerializeField] string challengesFailedText;

    public UnityEvent OnChallengePassed = new();

    [SerializeField] private Challenge startChallenge;
    [SerializeField] private List<Challenge> challenges;
    [SerializeField] private List<CardType> allResponses = new();

    private List<CardType> remainingResponses = new();

    private Challenge currentChallenge;

    bool waitingForNextLine = false;
    bool finished = false;

    private List<CardType> currentCards = new();
    public List<CardType> CurrentCards => currentCards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainingResponses.AddRange(allResponses);
        dialogContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogContainer.activeInHierarchy)
            return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(waitingForNextLine)
            {
                StartNextChallenge();
            }
            else if(finished)
            {
                ExitConverstaion();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitConverstaion();
        }
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
            currentChallenge = startChallenge;
            writer.OnWritingDone.AddListener(OnWritingDone);
            CardSystem.Instance.CardCanBePlayed = true;
        }
        else
        {
            if(challenges.Count(c => c.passed) + (startChallenge.passed ? 1 : 0) >= correctAnswersRequired)
            {
                OnChallengePassed.Invoke();
                writer.Write(challengesPassedText);
                finished = true;
            }
            else if(challenges.Any(c => !c.asked))
            {
                StartNextChallenge();
            }
            else
            {   
                writer.Write(challengesFailedText);
                finished = true;
            }
        }

        CardSystem.Instance.StartConversation(this);

    }

    private void OnWritingDone()
    {
        writer.OnWritingDone.RemoveListener(OnWritingDone);
    }

    private void StartNextChallenge()
    {
        waitingForNextLine = false;

        if(challenges.Count(c => c.passed) + (startChallenge.passed ? 1 : 0) >= correctAnswersRequired)
        {
            OnChallengePassed.Invoke();
            writer.Write(challengesPassedText);
            finished = true;
            return;
        }

        currentChallenge = challenges.FirstOrDefault(c => !c.asked);

        if(currentChallenge == null)
        {
            writer.Write(challengesFailedText);
            finished = true;
            return;
        }

        writer.Write(currentChallenge.Line);
        CardSystem.Instance.CardCanBePlayed = true;
        writer.OnWritingDone.AddListener(OnWritingDone);
        waitingForNextLine = false;
    }

    private void ExitConverstaion()
    {
        camera.Priority = 0;
        Player.Instance.PlayerMovement.CanMove = true;
        dialogContainer.SetActive(false);
        CardSystem.Instance.ClearHand();
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
        currentCards.Add(card);
        return card;
    }

    public void OnCardSelected(CardType type)
    {
        currentChallenge.asked = true;
        currentChallenge.passed = currentChallenge.correctResponse == type;

        writer.Write(currentChallenge.passed ? currentChallenge.correctResponseText : currentChallenge.incorrectResponseText);
        writer.OnWritingDone.AddListener(OnWritingDone);
        waitingForNextLine = true;
        currentCards.Remove(type);
    }
}

[Serializable]
public class Challenge
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