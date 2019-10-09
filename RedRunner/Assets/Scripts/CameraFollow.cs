using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    private Transform playerTransform;
    private SpriteRenderer playerSprite;
    
        public float cameraDistance = 120.0f;

    void Awake ()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);

        playerTransform = player.GetComponent<Transform>();
        playerSprite = player.GetComponent<SpriteRenderer>();

    }

    void FixedUpdate ()
    {
        if (playerSprite.enabled)
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}
