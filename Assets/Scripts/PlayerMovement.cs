using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float _movementSpeed = 1f;

    MovementAnimator animator;

    public bool CanMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator =  GetComponent<MovementAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {
            var input = Input.GetAxisRaw("Horizontal");
            transform.position += Vector3.right * input *_movementSpeed * Time.deltaTime;

            if(Mathf.Abs(input) < 0.1f)
            {
                animator.Stop();
            }   
            else
            {
                animator.Animate(input < 0 ? -1 : 1);
            } 
        }
        else
        {
            animator.Stop();
        }
    }
}
