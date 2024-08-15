using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    public float speed = 5f;
    private NetworkVariable<int> RandomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    void Start()
    {

    }

    void Update()
    {
        if (!IsOwner) return;

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3 (moveH, 0, moveV) * speed * Time.deltaTime;

        //transform.position = moveDir;
        transform.Translate(moveDir, Space.World);

        Debug.Log("Client id: " + OwnerClientId + " ... number: " + RandomNumber.Value);
        if (Input.GetKeyDown(KeyCode.N)){
        RandomNumber.Value = Random.Range(0, 100);
        }
    }
}
