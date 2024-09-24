using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    public GameObject CreditsPanel;  // Panel de créditos a mostrar

    public void OnCreditsButtonClick()
    {
        CreditsPanel.SetActive(!CreditsPanel.activeSelf);
    }
}