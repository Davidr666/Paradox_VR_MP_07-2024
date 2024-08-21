using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    public int speed;
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    // Start is called before the first frame update
    void Start()
    {
        new NetworkVariable<int>(1);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsOwner) { return; }
        var moveH = Input.GetAxis("Horizontal");
        var moveV = Input.GetAxis("Vertical");
        var moveDir = Vector3.zero;
        transform.position = moveDir * speed * Time.deltaTime;
        Debug.Log("Client id: " + OwnerClientId + " ... number: " + randomNumber.Value);
        //if (Input.GetKeyDown(KeyCode.N) {
          //  randomNumber.Value = Random.Range(0, 100);
        //}
    }
}
