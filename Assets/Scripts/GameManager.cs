using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore;
    [SerializeField] private GameObject wonUI;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject sphereObject;

    // The static instance that allows access to this class from anywhere.
    public static GameManager Instance { get; private set; }

    // Called when the script instance is being loaded.
    private void Awake()
    {
        // If the instance is not set yet, set it to this instance.
        if (Instance == null)
        {
            Instance = this;
            // Prevent the object from being destroyed on scene load.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one to enforce the Singleton.
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        Init();
    }
    
    private void Init()
    {
        playerScore = 0;
        wonUI.SetActive(false);
        startUI.SetActive(true);
    }
    
    public void GamePaused()
    {
        Time.timeScale = 0;
    }

    public void GameResumed()
    {
        Time.timeScale = 1;
    }

    public void GameWon()
    {
        GamePaused();
        
    }

    public void LoadNewScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void UnLoadNewScene(int sceneIndex)
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
    }
}
