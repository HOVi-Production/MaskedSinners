using Unity.Cinemachine;
using UnityEngine;

public class CardSystem : MonoBehaviour
{

    public static CardSystem Instance;

    [SerializeField] CinemachineCamera _conversationCamera;

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
        
    }
}
