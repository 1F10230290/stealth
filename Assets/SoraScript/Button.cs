using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public  void OnClickedStartButton()
    {
        SceneManager.LoadScene("sample");
    }

    public void OnClickedReplayButton()
    {
        SceneManager.LoadScene("sample");
    }
    public void OnClickedExitButton()
    {
        SceneManager.LoadScene("Title");
    }
}
