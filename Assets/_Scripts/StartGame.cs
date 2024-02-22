using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    private void Start()
    {
        MMDontDestroyOnLoad[] objectsWithComponent = FindObjectsOfType<MMDontDestroyOnLoad>();

        foreach (var obj in objectsWithComponent)
        {
            // Do something with the objects that have the desired component
            Destroy(obj.gameObject);
        }
    }

    public void StartGameButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
