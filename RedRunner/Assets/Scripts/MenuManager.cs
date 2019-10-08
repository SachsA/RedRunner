using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
