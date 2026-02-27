using NUnit.Framework;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject manualPanel;
    public TextMeshProUGUI buttonText;
    private Player_Move player_Move;
    

    // ボタンが押された時に実行するメソッド
    public void ToggleManual()
    {
        player_Move = GameObject.Find("Player").GetComponent<Player_Move>();
        if (manualPanel != null)
        {
            // 現在の状態の「逆」に設定する（開いていれば閉じ、閉じていれば開く）
            bool isActive = manualPanel.activeSelf;
            if(isActive)
            {
                player_Move.canMove = true; // プレイヤーの移動を許可
                Time.timeScale = 1f; // ゲームを再開
                buttonText.text = "操作説明書"; // ボタンのテキストを「操作説明書」に変更
            }
            else
            {
                player_Move.canMove = false; // プレイヤーの移動を禁止
                Time.timeScale = 0f; // ゲームを停止
                buttonText.text = "とじる"; // ボタンのテキストを「とじる」に変更
            }
            manualPanel.SetActive(!isActive);
        }
    }
}

