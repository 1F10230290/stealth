using UnityEngine;
using UnityEngine.EventSystems; //UIイベントのために

//IPointerEnterHandler (マウスが入った時)
//IPointerExitHandler (マウスが出た時)
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleFactor = 1.1f; //どれくらい大きくするか
    private Vector3 originalScale; //元の大きさを保存する変数
    void Start()
    {
        originalScale = transform.localScale; //元の大きさを保存
    }

    //カーソルがボタンの上に乗ったとき
    public void OnPointerEnter(PointerEventData eventData)
    {
        //ボタンを大きくする
        transform.localScale = originalScale * scaleFactor;
    }

    //カーソルがボタンから離れたとき
    public void OnPointerExit(PointerEventData eventData)
    {
        //ボタンを元の大きさに戻す
        transform.localScale = originalScale;
    }

    //クリックされた時にサイズを戻す
    private void OnDisable()
    {
        transform.localScale = originalScale;
    }
}
