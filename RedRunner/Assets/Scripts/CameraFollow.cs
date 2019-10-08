using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float cameraDistance = 120.0f;

    void Awake ()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    void FixedUpdate ()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
