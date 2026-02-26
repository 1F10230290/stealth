using UnityEngine;

public class Items : MonoBehaviour
{
    public int itemNum;
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6) // プレイヤーのレイヤー番号を指定
        {
            ItemManager itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
            itemManager.GetItem(itemNum);
            Destroy(gameObject);
        }
    }
}
