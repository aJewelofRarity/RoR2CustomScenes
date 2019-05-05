using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Turret : MonoBehaviour
{
    public int Price = 35;
    public RoR2.CostType costType = RoR2.CostType.Money;
    private void Awake()
    {
        if (NetworkServer.active)
        {
            GameObject Turret = Object.Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/networkedobjects/turret1Broken"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            RoR2.PurchaseInteraction purchaseInteraction = Turret.GetComponent<RoR2.PurchaseInteraction>();
            purchaseInteraction.costType = costType;
            purchaseInteraction.cost = Price;
            NetworkServer.Spawn(Turret);
        }
        DestroyImmediate(transform.GetChild(0).gameObject);
        DestroyImmediate(this.gameObject);
    }
}
