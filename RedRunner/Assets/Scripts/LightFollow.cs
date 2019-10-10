using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    public GameObject player;

    private Transform playerTransform;
    private SpriteRenderer playerSprite;

    void Awake ()
    {
        GetComponent<Light>();

        playerTransform = player.GetComponent<Transform>();
        playerSprite = player.GetComponent<SpriteRenderer>();

    }

    void FixedUpdate ()
    {
        if (playerSprite.enabled)
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}
