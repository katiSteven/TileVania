using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelExitSlowMoFactor = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>()) {
            StartCoroutine(ExitAfterSometime());
        }
    }

    IEnumerator ExitAfterSometime() {
        Time.timeScale = levelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;
        var currentSceneIndex = EditorSceneManager.GetActiveScene().buildIndex;
        EditorSceneManager.LoadScene(currentSceneIndex + 1);
    }

}
