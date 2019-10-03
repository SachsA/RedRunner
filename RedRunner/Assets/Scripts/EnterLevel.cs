using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject star_1;
    [SerializeField]
    private GameObject star_2;
    [SerializeField]
    private GameObject star_3;

    [SerializeField]
    private GameObject keyToEnter;

    private bool isPlayerInFront;

    void Start()
    {
        isPlayerInFront = false;
        keyToEnter.SetActive(false);
        star_1.SetActive(false);
        star_2.SetActive(false);
        star_3.SetActive(false);
        SetDoorStars(LevelsManager.Instance.GetStarsCollected(gameObject.name));
    }

    private void FixedUpdate()
    {
        if (isPlayerInFront && Input.GetAxis("Vertical") > 0)
        {
            SceneManager.LoadScene(gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInFront = true;
            keyToEnter.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInFront = false;
            keyToEnter.SetActive(false);
        }
    }

    void SetDoorStars(int nbStars)
    {
        if (nbStars >= 1)
        {
            star_1.SetActive(true);
        }
        if (nbStars >= 2)
        {
            star_2.SetActive(true);
        }
        if (nbStars == 3)
        {
            star_3.SetActive(true);
        }
    }
}
