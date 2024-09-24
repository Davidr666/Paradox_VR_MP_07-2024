using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetsgoButton : MonoBehaviour
{
    public void OnLetsgoButtonClick()
    {
        SceneManager.LoadScene("VR_Scene_JumpiPig");  // Cargar la escena del juego
    }
}
