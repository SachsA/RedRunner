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

    #endregion

    #region Private Fields

    private Dictionary<string, int> starsCollected = new Dictionary<string, int>();

    [SerializeField]
    private AudioSource AudioSource;

    private int Life = 5;

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (AudioSource.isPlaying)
        {
            return;
        }
        AudioSource.loop = true;
        AudioSource.Play();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver" && Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
            SceneManager.LoadScene("SelectLevel");
        }
    }

    #endregion

    #region Public Methods

    public void SetStarsCollected(string levelName, int nbStars)
    {
        starsCollected[levelName] = nbStars;
    }

    public void RemoveOneLife()
    {
        Life -= 1;
        if (Life == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public int GetLife()
    {
        return Life;
    }

    public int GetStarsCollected(string levelName)
    {
        if (starsCollected.ContainsKey(levelName))
        {
            return starsCollected[levelName];
        }
        return 0;
    }

    public void ResetGame()
    {
        Life = 5;
        starsCollected.Clear();
    }

    #endregion
}
