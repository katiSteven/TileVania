using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text ScoreText;

    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        livesText.text = playerLives.ToString();
        ScoreText.text = score.ToString();
	}

    public void AddToScore(int pointsToAdd) {
        score += pointsToAdd;
        ScoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath() {
        if (playerLives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(RestartGameSession());
        }
    }

    IEnumerator TakeLife()
    {
        yield return new WaitForSecondsRealtime(2f);
        --playerLives;
        livesText.text = playerLives.ToString();
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator RestartGameSession()
    {
        yield return new WaitForSecondsRealtime(2f);
        EditorSceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
