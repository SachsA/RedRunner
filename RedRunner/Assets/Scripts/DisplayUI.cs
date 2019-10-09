using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUi : MonoBehaviour
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

    public void OnChangeCoins(int totalCoins)
    {
        coinsText.text = totalCoins + "/" + levelManager.GetTotalCoins();
    }

    public void OnChangeHearts(int totalHearts)
    {
        heartsText.text = "X" + totalHearts;
    }
}
