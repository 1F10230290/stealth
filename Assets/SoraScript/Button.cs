using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameManager gameManager;

    public  void OnClickedStartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("sample");
    }

    public void OnClickedReplayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("sample");
    }
    public void OnClickedExitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
    public void OnClickedGameStartButton()
    {
        gameManager.GameStart();
    }
}
