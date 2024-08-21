using System;
using UnityEngine;
public class EventManager : MonoBehaviour
{
    // Event declaration
    public static event Action OnActionTriggered;
    public void TriggerActionOnButtonClicked()
    {
        OnActionTriggered?.Invoke();
    }
}
