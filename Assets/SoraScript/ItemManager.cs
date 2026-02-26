using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Image[] itemImages;
    public GameManager gameManager;
    private bool[] items = new bool[3];
    private int itemCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        items = new bool[itemImages.Length];

        for (int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].color = new Color32(89, 89, 89, 255);
        }
    }

    public void GetItem(int itemNum)
    {
        // アイテム番号は1から始まると仮定
        int index = itemNum - 1;

        // すでに取得しているアイテムの場合は何もしない
        if(items[index]) return;

        // アイテムを取得
        items[index] = true;

        // アイテム画像を通常の色に変更
        itemImages[index].color = new Color32(255, 255, 255, 255);

        itemCount++; // アイテム取得数をカウント

        // すべてのアイテムを取得したかチェック
        if(itemCount >= items.Length)
        {
            gameManager.GameClear();
        }
    }
}
