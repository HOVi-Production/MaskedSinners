using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
