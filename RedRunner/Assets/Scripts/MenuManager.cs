using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuCanvas;

    [SerializeField]
    private GameObject SettingsCanvas;

    private bool isSettings = false;

    void Start()
    {
        isSettings = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void ChangeCanvas()
    {
        isSettings = !isSettings;
        if (isSettings)
        {
            SettingsCanvas.SetActive(true);
            MenuCanvas.SetActive(false);
        } else
        {
            SettingsCanvas.SetActive(false);
            MenuCanvas.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
