using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

//[DisallowMultipleComponents]
[DisallowMultipleComponent]
public class ClientNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

}