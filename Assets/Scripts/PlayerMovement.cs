using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float _movementSpeed = 1f;

    public bool CanMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {
            transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") *_movementSpeed * Time.deltaTime;    
        }
    }
}
