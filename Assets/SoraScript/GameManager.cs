using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image GameCrearImage;
    public Image GameOverImage;
    public Player_Move player_Move;
    public bool finished = false;
    public GameObject introductionPanel;

    void Start()
    {
        finished = false;
        player_Move.canMove = false;
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

    public void GameStart()
    {
        introductionPanel.gameObject.SetActive(false);
        player_Move.canMove = true;
    }
}
