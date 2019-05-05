using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnPointCreate : MonoBehaviour
{
    private void Awake()
    {
        if (NetworkServer.active)
        {
            gameObject.AddComponent<RoR2.SpawnPoint>();
            gameObject.transform.GetChild(0).gameObject.AddComponent<DisableOnStart>();
        }
        Destroy(this.gameObject.GetComponent<SpawnPointCreate>());
    }
}
