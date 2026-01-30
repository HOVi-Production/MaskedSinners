using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera;
    [SerializeField] private Transform playerConversationPos;

    [SerializeField] private Challenge startChallenge;
    [SerializeField] private List<Challenge> challenges;
    [SerializeField] private List<CardType> allResponses;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

        CardSystem.Instance.StartConversation();
    }
}

[Serializable]
public struct Challenge
{
    public string Line;
    public CardType correctResponse;
    public string correctResponseText;
    public string incorrectResponseText;
    public bool asked;
    public bool passed;
}