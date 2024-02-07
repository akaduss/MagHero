using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public static LoadNextLevel Instance;

    public event Action<Vector3> OnLoadNextScene;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager.OnAllEnemiesDefeated += GameManager_OnAllEnemiesDefeated;
        gameObject.SetActive(false);
    }

    private void GameManager_OnAllEnemiesDefeated()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the total number of scenes
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If there is no next scene, you can handle it accordingly
            Debug.LogWarning("No next scene available.");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //// Check if the loaded scene is the target scene
        //if (scene.name == nextSceneName)
        //{
        // Retrieve the spawn position from the static variable
        if (mode == LoadSceneMode.Single) { print("ste"); }
        Vector3 spawnPosition = new(0, 2.6f, -15f);

        OnLoadNextScene?.Invoke(spawnPosition);
        gameObject.GetComponent<Collider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}