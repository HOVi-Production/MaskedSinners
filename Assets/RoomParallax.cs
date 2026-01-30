using UnityEngine;

public class RoomParallax : MonoBehaviour
{
    [SerializeField] Transform leftWall;
    private float leftScale;
    [SerializeField] Transform rightWall;
    private float rightScale;

    [SerializeField] float parralaxStrength;

    private Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
        leftScale = leftWall.localScale.x;
        rightScale = rightWall.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        var rightParallax = Mathf.Lerp(0, rightScale, Mathf.Clamp((rightWall.position.x - camera.transform.position.x) / parralaxStrength, 0, 1));
        rightWall.localScale = new Vector3(rightParallax, rightWall.localScale.y, 1);

        
        var leftParallax = Mathf.Lerp(0, leftScale, Mathf.Clamp((camera.transform.position.x- leftWall.position.x) / parralaxStrength, 0, 1));
        leftWall.localScale = new Vector3(leftParallax, leftWall.localScale.y, 1);
    }
}
