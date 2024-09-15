using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Start()
    {
        Move();
        Fire();
        Damage();
    }

    public void Move()
    {
        Debug.Log("the GameObject names is:" + gameObject.name);
        Debug.Log("the Method names is:Move");
    }

    public void Fire()
    {
        Debug.Log("the GameObject names is:" + gameObject.name + " , the Method name is Fire");
    }

    public void Damage()
    {
        Debug.Log("the GameObject names is:" + gameObject.name + "the Method names is Damage");
    }
}
