using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    private Camera myCam;
    public float camSize;


    private void Start()
    {
        myCam = GetComponent<Camera>();
        if (camSize != 0)
        {
            myCam.orthographicSize = camSize;
        }
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update() 
    {
        gameObject.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);
    }
}
