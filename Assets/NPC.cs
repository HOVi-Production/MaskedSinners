using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera;
    [SerializeField] private Transform playerConversationPos;


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
    }
}
