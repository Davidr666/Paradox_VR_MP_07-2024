using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Letsgo()
    {
        SceneManager.LoadScene("VR_Scene_JumpiPig");
    }
        
    public void Credits()
    {
        // Abrir un panel o cargar una escena de configuraciones.
    }

    // Método para salir del juego
    public void Exit()
    {
        Application.Quit(); 
    }
}
