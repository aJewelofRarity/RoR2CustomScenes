using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class Chest : MonoBehaviour
{
    public int Price = 25;
    public enum ChestType
    {
        Small,
        Large,
        Gold,
        Equipment,
        Cloaked,
        Lockbox,
        Lunar
    }
    public RoR2.CostType costType = RoR2.CostType.Money;
    public ChestType chestType = ChestType.Small;
    public float Tier1Chance = 0.8f;
    public float Tier2Chance = 0.2f;
    public float Tier3Chance = 0.01f;
    public float LunarChance = 0f;
    private string realChestType;
    private void Awake()
    {
        if (NetworkServer.active)
        {
            switch (chestType)
            {
                case ChestType.Small: realChestType = "prefabs/networkedobjects/chest1"; break;
                case ChestType.Large: realChestType = "prefabs/networkedobjects/chest2"; break;
                case ChestType.Gold: realChestType = "prefabs/networkedobjects/goldchest"; break;
                case ChestType.Equipment: realChestType = "prefabs/networkedobjects/equipmentbarrel"; break;
                case ChestType.Cloaked: realChestType = "prefabs/networkedobjects/chest1stealthed"; break;
                case ChestType.Lockbox: realChestType = "prefabs/networkedobjects/lockbox"; break;
                case ChestType.Lunar: realChestType = "prefabs/networkedobjects/lunarchest"; break;
                default: realChestType = "prefabs/networkedobjects/chest1"; break;
            }
            GameObject Chest = Object.Instantiate<GameObject>(Resources.Load<GameObject>(realChestType), transform.GetChild(0).position, transform.GetChild(0).rotation);
            RoR2.PurchaseInteraction purchaseInteraction = Chest.GetComponent<RoR2.PurchaseInteraction>();
            RoR2.ChestBehavior chestBehavior = Chest.GetComponent<RoR2.ChestBehavior>();
            chestBehavior.tier1Chance = Tier1Chance;
            chestBehavior.tier2Chance = Tier2Chance;
            chestBehavior.tier3Chance = Tier3Chance;
            chestBehavior.lunarChance = LunarChance;
            purchaseInteraction.costType = costType;
            purchaseInteraction.cost = Price;
            NetworkServer.Spawn(Chest);
        }
        DestroyImmediate(transform.GetChild(0).gameObject);
        DestroyImmediate(this.gameObject);
    }
}
