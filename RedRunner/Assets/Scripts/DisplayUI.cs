using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    public LevelManager levelManager;

    public Text coinsText;
    public Text heartsText;

    void Start()
    {
        OnChangeCoins(levelManager.GetCollectedCoins());
        OnChangeHearts(LevelsManager.Instance.GetLife());
    }

    private void FixedUpdate()
    {
        OnChangeCoins(levelManager.GetCollectedCoins());
        OnChangeHearts(LevelsManager.Instance.GetLife());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToSelectLevel();
        }
    }

    public void OnChangeCoins(int totalCoins)
    {
        coinsText.text = totalCoins + "/" + levelManager.GetTotalCoins();
    }

    public void OnChangeHearts(int totalHearts)
    {
        heartsText.text = "X" + totalHearts;
    }

    public void ReturnToSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
