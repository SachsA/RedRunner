using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Transform Player;

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
        SavePlayerPos = Player.position;
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
    }

    public void PlayerIsDead()
    {
        Player.position = SavePlayerPos;
        PlayerDeadOnce = true;
    }
}
