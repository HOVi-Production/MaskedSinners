using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera;
    [SerializeField] private Transform playerConversationPos;
    [SerializeField] private GameObject dialogContainer;
    [SerializeField] private Writer writer;
    
    [SerializeField] private string question;
    [SerializeField] private string noOneToAccuse;

    public static Boss Instance;

    public bool canAccuse1 = false;
    public bool canAccuse2 = false;
    public bool canAccuse3 = false;

    [SerializeField] private GameObject accuse1Prefab;
    [SerializeField] private GameObject accuse2Prefab;
    [SerializeField] private GameObject accuse3Prefab;

    void Start()
    {
        Instance = this;
        dialogContainer.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            camera.Priority = 0;
            Player.Instance.PlayerMovement.CanMove = true;
            dialogContainer.SetActive(false);
            CardSystem.Instance.ClearHand();
        }
    }

    public void SetAccuse1()
    {
        canAccuse1 = true;
    }

    public void SetAccuse2()
    {
        canAccuse2 = true;
    }

    public void SetAccuse3()
    {
        canAccuse3 = true;
    }
    
    public void StartConversation()
    {
        camera.Priority = 2;

        Player.Instance.transform.DOMove(playerConversationPos.position, 0.2f);
        Player.Instance.PlayerMovement.CanMove = false;

        dialogContainer.SetActive(true);

        if(!canAccuse1 && !canAccuse2 && !canAccuse3)
        {
            writer.Write(noOneToAccuse);
            return;
        }

        writer.Write(question);

        if(canAccuse1)
        {
            CardSystem.Instance.AddCardToHand<Card>(accuse1Prefab, (_) => {});
        }
        if(canAccuse2)
        {
            CardSystem.Instance.AddCardToHand<Card>(accuse2Prefab, (_) => {});
        }
        if(canAccuse3)
        {
            CardSystem.Instance.AddCardToHand<Card>(accuse3Prefab, (_) => {});
        }
    }

}
