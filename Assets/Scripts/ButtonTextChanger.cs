using UnityEngine;
using UnityEngine.UI;
public class ButtonTextChanger : MonoBehaviour
{
    public Button targetButton;
    private void OnEnable()
    {

        EventManager.OnActionTriggered += ChangeButtonText;
    }
    private void OnDisable()
    {
        EventManager.OnActionTriggered -= ChangeButtonText;
    }
    private void ChangeButtonText()
    {
        targetButton.GetComponentInChildren<Text>().text =
       "Button Clicked!";
    }
}

