using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pickup : MonoBehaviour
{
    public RoR2.ItemIndex itemIndex;
    public bool IsRandom;
    private void Awake()
    {
        if (NetworkServer.active)
        {
            if (!IsRandom)
            {
                RoR2.PickupDropletController.CreatePickupDroplet(new RoR2.PickupIndex(itemIndex), transform.position, Vector3.zero);
            }
            else
            {
                RoR2.PickupDropletController.CreatePickupDroplet(new RoR2.PickupIndex((RoR2.ItemIndex)Random.Range(0, (int)RoR2.ItemIndex.Count)), transform.position, Vector3.zero);
            }
        }
        DestroyImmediate(this.gameObject);
    }
}
