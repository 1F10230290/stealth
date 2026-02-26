using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image GameCrearImage;
    public Image GameOverImage;

    void Start()
    {
        GameCrearImage.gameObject.SetActive(false);
        GameOverImage.gameObject.SetActive(false);
    }

    public void GameClear()
    {
        Debug.Log("ゲームクリア");
        GameCrearImage.gameObject.SetActive(true);
        Time.timeScale = 0f; // ゲームを停止
    }

    public void GameOver()
    {
        Debug.Log("ゲームオーバー");
        GameOverImage.gameObject.SetActive(true);
        Time.timeScale = 0f; // ゲームを停止
    }
}
