using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{

    #region Public Fields

    public Sprite FrontDoor_Yellow;
    public Sprite FrontDoor_Grey;
    public Sprite FrontDoor_DarkGrey;
    public Sprite Star;

    public static LevelsManager Instance;

    // Or Private ?
    public int Life = 5;

    #endregion

    #region Private Fields

    private Dictionary<string, int> starsCollected = new Dictionary<string, int>();

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;
    }

    #endregion

    #region Public Methods

    public void StartGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void SetStarsCollected(string levelName, int nbStars)
    {
        starsCollected.Add(levelName, nbStars);
    }

    public void RemoveOneLife()
    {
        Life -= 1;
    }

    public int GetStarsCollected(string levelName)
    {
        if (starsCollected.ContainsKey(levelName))
        {
            return starsCollected[levelName];
        }
        return 0;
    }

    #endregion
}
