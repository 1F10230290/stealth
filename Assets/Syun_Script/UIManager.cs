using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject Operation_instructions;

    bool isOpen = false;

    public void ToggleHelp()
    {
        isOpen = !isOpen;
        Operation_instructions.SetActive(isOpen);
    }
}
