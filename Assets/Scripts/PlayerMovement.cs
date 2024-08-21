using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform myTransform;
    public Vector3 position1;
    public float speed = 5f;

    private void Start()
    {
        myTransform = transform;
        transform.position = myTransform.position;

        position1 = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        var moveH = Input.GetAxis("Horizontal");
        var moveV = Input.GetAxis("Vertical");  

        var newPos = new Vector3(moveH,0,moveV);

        transform.Translate(newPos * speed * Time.deltaTime);
    }
}
