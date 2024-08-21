using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    public Button targetButton;

    private void OnEnable()
    {
        EventManager.OnActionTriggered += ChangeButtonColor;
    }

    private void OnDisable()
    {
        EventManager.OnActionTriggered -= ChangeButtonColor;
    }

    private void ChangeButtonColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value);
        targetButton.GetComponent<Image>().color = newColor;
    }
}