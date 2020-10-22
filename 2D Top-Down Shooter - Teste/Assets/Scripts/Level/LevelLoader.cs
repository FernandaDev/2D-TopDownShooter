using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
