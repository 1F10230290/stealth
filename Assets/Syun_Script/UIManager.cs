using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject manualPanel;

    // ボタンが押された時に実行するメソッド
    public void ToggleManual()
    {
        if (manualPanel != null)
        {
            // 現在の状態の「逆」に設定する（開いていれば閉じ、閉じていれば開く）
            bool isActive = manualPanel.activeSelf;
            manualPanel.SetActive(!isActive);
        }
    }
}

