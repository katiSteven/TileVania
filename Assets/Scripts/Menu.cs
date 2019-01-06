using UnityEngine;
using UnityEditor.SceneManagement;

public class Menu : MonoBehaviour {

    public void StartFirstLevel() {
        EditorSceneManager.LoadScene(1);
    }

    public void BackToMenu() {
        EditorSceneManager.LoadScene(0);
    }
}
