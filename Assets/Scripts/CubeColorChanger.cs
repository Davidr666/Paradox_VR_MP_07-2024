using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    public GameObject targetCube;

    private void OnEnable()
    {
        EventManager.OnActionTriggered += ChangeCubeColor;
    }

    private void OnDisable()
    {
        EventManager.OnActionTriggered -= ChangeCubeColor;
    }

    private void ChangeCubeColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value);
        targetCube.GetComponent<Renderer>().material.color = newColor;
    }
}