using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image GameCrearImage;
    public Image GameOverImage;
    public bool finished = false;

    void Start()
    {
        finished = false;
        Time.timeScale = 1;
        GameCrearImage.gameObject.SetActive(false);
        GameOverImage.gameObject.SetActive(false);
    }

    public void GameClear()
    {
        if (finished) return;
        Debug.Log("ゲームクリア");
        Debug.Log(finished);
        finished = true;
        GameCrearImage.gameObject.SetActive(true);
        Invoke(nameof(TimeStop),1f);
    }

    public void GameOver()
    {
        if(finished == true) return;
        Debug.Log("ゲームオーバー");
        Debug.Log(finished);
        finished = true;
        GameOverImage.gameObject.SetActive(true);
        Invoke(nameof(TimeStop),1f);

    }

    private void TimeStop()
    {
        Time.timeScale = 0f;
    }
}
