using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private int TotalCoins;
    private int CollectedCoins;
    private bool PlayerDeadOnce;
    private Vector2 SavePlayerPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerDeadOnce = false;
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void AddOneCoin()
    {
        CollectedCoins += 1;
    }

    public void ChestFound()
    {
        int nbStars = 1;
        
        if (CollectedCoins == TotalCoins)
        {
            nbStars += 1;
        }
        if (!PlayerDeadOnce)
        {
            nbStars += 1;
        }
        LevelsManager.Instance.SetStarsCollected(SceneManager.GetActiveScene().name, nbStars);
        StartCoroutine("WaitToEnd");
    }

    IEnumerator WaitToEnd()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("SelectLevel");
    }

    public void PlayerIsDead()
    {
        PlayerDeadOnce = true;
    }
}
