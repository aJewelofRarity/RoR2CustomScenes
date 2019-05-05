using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drone : MonoBehaviour
{
    public int Price = 35;
    public RoR2.CostType costType = RoR2.CostType.Money;
    public enum DroneType
    {
        Gunner,
        Healing,
        Missile,
        Mega
    }
    public DroneType droneType;
    private void Awake()
    {
        if (NetworkServer.active)
        {
            string DroneString;
            switch (droneType)
            {
                case DroneType.Gunner: DroneString = "prefabs/networkedobjects/drone1broken"; break;
                case DroneType.Healing: DroneString = "prefabs/networkedobjects/drone2broken"; break;
                case DroneType.Missile: DroneString = "prefabs/networkedobjects/missiledronebroken"; break;
                case DroneType.Mega: DroneString = "prefabs/networkedobjects/megadronebroken"; break;
                default: DroneString = "prefabs/networkedobjects/Drone1Broken"; break;
            }
            GameObject Drone = Object.Instantiate<GameObject>(Resources.Load<GameObject>(DroneString), transform.GetChild(0).position, transform.GetChild(0).rotation);
            RoR2.PurchaseInteraction purchaseInteraction = Drone.GetComponent<RoR2.PurchaseInteraction>();
            purchaseInteraction.costType = costType;
            purchaseInteraction.cost = Price;
            NetworkServer.Spawn(Drone);
        }
        DestroyImmediate(transform.GetChild(0).gameObject);
        DestroyImmediate(this.gameObject);
    }
}
