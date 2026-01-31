using UnityEngine;

public class RoomParallax : MonoBehaviour
{
    [SerializeField] Transform leftWall;
    private float leftScale;
    [SerializeField] Transform leftWallInterior;
    private float leftInteriorPos;
    [SerializeField] Transform rightWall;
    private float rightScale;
    [SerializeField] Transform rightWallInterior;
    private float rightInteriorPos;

    [SerializeField] float parralaxStrength;

    private Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
        leftScale = leftWall.localScale.x;
        rightScale = rightWall.localScale.x;

        leftInteriorPos = leftWallInterior.position.x;
        rightInteriorPos = rightWallInterior.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        var rightParrallax = Mathf.Clamp((rightWall.position.x - camera.transform.position.x) / parralaxStrength, 0, 1);
        var rightSqew = Mathf.Lerp(0, rightScale, rightParrallax);
        rightWall.localScale = new Vector3(rightSqew, rightWall.localScale.y, 1);
        
        rightWallInterior.position =  new Vector3(rightInteriorPos + Mathf.Lerp(0, rightScale, rightParrallax), rightWallInterior.position.y, rightWallInterior.position.z);
        rightWallInterior.gameObject.SetActive(rightWall.position.x - camera.transform.position.x > -1);
        
        var leftParrallax = Mathf.Clamp((camera.transform.position.x - leftWall.position.x) / parralaxStrength, 0, 1);
        var leftSqew = Mathf.Lerp(0, leftScale, leftParrallax);
        leftWall.localScale = new Vector3(leftSqew, leftWall.localScale.y, 1);
        
        leftWallInterior.position =  new Vector3(leftInteriorPos + Mathf.Lerp(0, -leftScale, leftParrallax), leftWallInterior.position.y, leftWallInterior.position.z);
        leftWallInterior.gameObject.SetActive(camera.transform.position.x - leftWall.position.x > -1);
    }
}
