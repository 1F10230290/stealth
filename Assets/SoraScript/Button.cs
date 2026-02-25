using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public  void OnClickedStartButton()
    {
        SceneManager.LoadScene("sample");
    }
}
