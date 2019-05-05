using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateGM : MonoBehaviour
{
    private void Awake()
    {
        if (NetworkServer.active)
        {
            gameObject.AddComponent<RoR2.GlobalEventManager>();
        }
        Destroy(this.gameObject.GetComponent<CreateGM>());
    }
}
