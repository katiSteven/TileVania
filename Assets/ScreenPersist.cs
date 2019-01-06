using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ScreenPersist : MonoBehaviour {


    int startingSceneIndex;

    private void Awake()
    {
        int numScreenPersist = FindObjectsOfType<ScreenPersist>().Length;
        if (numScreenPersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        startingSceneIndex = EditorSceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update () {
        if (startingSceneIndex != EditorSceneManager.GetActiveScene().buildIndex)
        {
            Destroy(gameObject);
        }
    }
}
