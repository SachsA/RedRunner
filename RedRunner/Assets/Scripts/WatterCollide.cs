using UnityEngine;
using UnityEngine.SceneManagement;

public class WatterCollide : MonoBehaviour
{
    private Vector2 playerPosAtStart;

    private void Start()
    {
        playerPosAtStart = GameObject.Find("Player").transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.localPosition = playerPosAtStart;
        }
    }
}
