using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float length;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3 (xPosition + distanceToMove, transform.position.y);
        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition+length;
        } else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }
    }
}
