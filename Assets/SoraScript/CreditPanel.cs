using UnityEngine;

public class CreditPanel : MonoBehaviour
{
    public GameObject CreditPanelObject;
    void Start()
    {
        CreditPanelObject.gameObject.SetActive(false);
    }
    public void OnCredit()
    {
        CreditPanelObject.gameObject.SetActive(true);
    }

    public void ExitCredit()
    {
        CreditPanelObject.gameObject.SetActive(false);
    }
}
