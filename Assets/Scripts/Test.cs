using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    //Declarar variable GameObject que hace referencia la cubo1 - varias variables
    //Crear un metodo para cambiarles el color a los game objects
    //Asignar el script al game object test en unity
    //Asignar las referencias
    //Asignar el game object en el evento OnClick del boton
    //Asignar el metodo ChangeColor

    [SerializeField] private GameObject cube1;
    [SerializeField] private GameObject cube2;
    [SerializeField] private GameObject cube3;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject planeObj;

    [SerializeField] private Button button2;

    public void ChangeColor()
    {
        cube1.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        cube2.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        cube3.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        planeObj.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        button1.gameObject.GetComponent<Image>().color = Color.yellow;
        button2.image.color = Color.yellow;
    }
}
