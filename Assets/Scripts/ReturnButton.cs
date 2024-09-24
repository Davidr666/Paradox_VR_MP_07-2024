using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    public void OnExitClick()
    {
        SceneManager.LoadScene("Menu");  // Cargar la escena del juego
    }
}
