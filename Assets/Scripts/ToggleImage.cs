using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleImage : MonoBehaviour
{
    public GameObject imageObject;

    private bool isImageVisible = false;

    public void ToggleImageVisibility()
    {
        isImageVisible = !isImageVisible; // Cambia el estado de visibilidad
        imageObject.SetActive(isImageVisible);
    }
}