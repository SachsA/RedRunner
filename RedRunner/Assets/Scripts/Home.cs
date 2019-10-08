using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public void BackHome()
    {
        SceneManager.LoadScene("Introduction");
    }
}
