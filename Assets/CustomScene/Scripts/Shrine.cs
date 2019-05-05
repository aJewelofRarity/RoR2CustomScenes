using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shrine : MonoBehaviour
{
    public enum CostType
    {
        None,
        Money,
        Health,
        Lunar,
        WhiteItem,
        GreenItem,
        RedItem
    }
    public enum ShrineType
    {
        Healing,
        Chance,
        Combat,
        Mountain,
        Order,
        Blood
    }
    [Header("General")]
    public int Price = 25;
    public RoR2.CostType costType = RoR2.CostType.Money;
    public float CostMultiplier = 1.5f;
    public int MaxPurchaseCount = 3;
    public ShrineType shrineType;
    private string realShrineType;
    [Header("Shrine of Healing")]
    public float BaseRadius = 12f;
    public float RadiusBonusPerPurchase = 8f;

    [Header("Shrine of Chance")]
    public float FailureWeight = 10.1f;
    public float EquipmentWeight = 2;
    public float Tier1Weight = 8;
    public float Tier2Weight = 2;
    public float Tier3Weight = 0.2f;

    [Header("Shrine of Combat")]
    public int BaseMonsterCredit = 100;
    public int MonsterCreditCoefficientPerPurchase = 2;

    [Header("Shrine of Blood")]
    public float GoldToHpRation = 0.5f;
    private void Awake()
    {
        if (NetworkServer.active)
        {
            switch (shrineType)
            {
                case ShrineType.Healing: realShrineType = "prefabs/networkedobjects/ShrineHealing"; break;
                case ShrineType.Chance: realShrineType = "prefabs/networkedobjects/ShrineChance"; break;
                case ShrineType.Combat: realShrineType = "prefabs/networkedobjects/ShrineCombat"; break;
                case ShrineType.Mountain: realShrineType = "prefabs/networkedobjects/ShrineBoss"; break;
                case ShrineType.Order: realShrineType = "prefabs/networkedobjects/ShrineRestack"; break;
                case ShrineType.Blood: realShrineType = "prefabs/networkedobjects/ShrineBlood"; break;
                default: realShrineType = "prefabs/networkedobjects/ShrineHealing"; break;
            }
            GameObject Shrine = Object.Instantiate<GameObject>(Resources.Load<GameObject>(realShrineType), transform.GetChild(0).position, transform.GetChild(0).rotation);
            RoR2.PurchaseInteraction purchaseInteraction = Shrine.GetComponent<RoR2.PurchaseInteraction>();
            if (Shrine.GetComponent<RoR2.ShrineHealingBehavior>())
            {
                RoR2.ShrineHealingBehavior component = Shrine.GetComponent<RoR2.ShrineHealingBehavior>();
                component.baseRadius = BaseRadius;
                component.radiusBonusPerPurchase = RadiusBonusPerPurchase;
                component.maxPurchaseCount = MaxPurchaseCount;
                component.costMultiplierPerPurchase = CostMultiplier;
            }
            if (Shrine.GetComponent<RoR2.ShrineChanceBehavior>())
            {
                RoR2.ShrineChanceBehavior component = Shrine.GetComponent<RoR2.ShrineChanceBehavior>();
                component.maxPurchaseCount = MaxPurchaseCount;
                component.costMultiplierPerPurchase = CostMultiplier;
                component.failureWeight = FailureWeight;
                component.equipmentWeight = EquipmentWeight;
                component.tier1Weight = Tier1Weight;
                component.tier2Weight = Tier2Weight;
                component.tier3Weight = Tier3Weight;
            }
            if (Shrine.GetComponent<RoR2.ShrineCombatBehavior>())
            {
                RoR2.ShrineCombatBehavior component = Shrine.GetComponent<RoR2.ShrineCombatBehavior>();
                component.maxPurchaseCount = MaxPurchaseCount;
                component.baseMonsterCredit = BaseMonsterCredit;
                component.monsterCreditCoefficientPerPurchase = MonsterCreditCoefficientPerPurchase;
            }
            if (Shrine.GetComponent<RoR2.ShrineBossBehavior>())
            {
                RoR2.ShrineBossBehavior component = Shrine.GetComponent<RoR2.ShrineBossBehavior>();
                component.maxPurchaseCount = MaxPurchaseCount;
                component.costMultiplierPerPurchase = CostMultiplier;
            }
            if (Shrine.GetComponent<RoR2.ShrineRestackBehavior>())
            {
                RoR2.ShrineRestackBehavior component = Shrine.GetComponent<RoR2.ShrineRestackBehavior>();
                component.maxPurchaseCount = MaxPurchaseCount;
                component.costMultiplierPerPurchase = CostMultiplier;
            }
            if (Shrine.GetComponent<RoR2.ShrineBloodBehavior>())
            {
                RoR2.ShrineBloodBehavior component = Shrine.GetComponent<RoR2.ShrineBloodBehavior>();
                component.maxPurchaseCount = MaxPurchaseCount;
                component.costMultiplierPerPurchase = CostMultiplier;
                component.goldToPaidHpRatio = GoldToHpRation;
            }
            purchaseInteraction.costType = costType;
            purchaseInteraction.cost = Price;
            NetworkServer.Spawn(Shrine);
        }
        DestroyImmediate(transform.GetChild(0).gameObject);
        DestroyImmediate(this.gameObject);
    }
}
