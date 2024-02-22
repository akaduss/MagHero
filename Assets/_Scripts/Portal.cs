using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public static event Action<Vector3> OnLoadNextScene;

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
            SceneManager.LoadScene(currentSceneIndex);
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
        // Check if the loaded scene is the target scene
        if (scene.isLoaded)
        {
            Vector3 spawnPosition = new(0, 2.6f, -15f);

            OnLoadNextScene?.Invoke(spawnPosition);
            gameObject.SetActive(false);
            GameManager.Instance.isPlayed = false;
        }
    }

}
