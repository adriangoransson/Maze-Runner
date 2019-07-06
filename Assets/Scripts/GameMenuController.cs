using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Ingame menu controller. Method calls are connected in Unity.
 */
public class GameMenuController : MonoBehaviour
{
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
