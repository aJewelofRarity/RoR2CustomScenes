using MultiMod.Interface;
using UnityEngine;
using UnityEngine.Networking;

public class Teleporter : ModBehaviour
{
    private void Awake()
    {
        if (NetworkServer.active)
        {
            GameObject Chest = Object.Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/networkedobjects/teleporter1"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            NetworkServer.Spawn(Chest);
        }
        DestroyImmediate(transform.GetChild(0).gameObject);
        DestroyImmediate(this.gameObject);
    }
}
