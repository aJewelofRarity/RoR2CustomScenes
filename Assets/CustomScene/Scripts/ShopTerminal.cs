using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShopTerminal : MonoBehaviour
{
    public RoR2.ItemTier itemTier;
    private bool hideDisplayContent = false;
    public int Price = 25;
    public RoR2.CostType costType = RoR2.CostType.Money;
    public enum TerminalType
    {
        Small,
        Large,
        Lunar,
        Cauldron
    }
    public TerminalType terminalType;
    public bool UseCustomIndex = false;
    public List<RoR2.ItemIndex> itemIndex;
    private void Awake()
    {
        if(NetworkServer.active)
        {
            RoR2.PickupIndex newPickupIndex = RoR2.PickupIndex.none;
            switch (this.itemTier)
            {
                case RoR2.ItemTier.Tier1:
                    newPickupIndex = RoR2.Run.instance.availableTier1DropList[RoR2.Run.instance.treasureRng.RangeInt(0, RoR2.Run.instance.availableTier1DropList.Count)];
                    break;
                case RoR2.ItemTier.Tier2:
                    newPickupIndex = RoR2.Run.instance.availableTier2DropList[RoR2.Run.instance.treasureRng.RangeInt(0, RoR2.Run.instance.availableTier2DropList.Count)];
                    break;
                case RoR2.ItemTier.Tier3:
                    newPickupIndex = RoR2.Run.instance.availableTier3DropList[RoR2.Run.instance.treasureRng.RangeInt(0, RoR2.Run.instance.availableTier3DropList.Count)];
                    break;
                case RoR2.ItemTier.Lunar:
                    newPickupIndex = RoR2.Run.instance.availableLunarDropList[RoR2.Run.instance.treasureRng.RangeInt(0, RoR2.Run.instance.availableLunarDropList.Count)];
                    break;
            }
            bool newHidden = hideDisplayContent && RoR2.Run.instance.treasureRng.nextNormalizedFloat < 0.2f;
            string ShopString;
            switch (terminalType)
            {
                case TerminalType.Small: ShopString = "prefabs/networkedobjects/multishopterminal"; break;
                case TerminalType.Large: ShopString = "prefabs/networkedobjects/multishoplargeterminal"; break;
                case TerminalType.Lunar: ShopString = "prefabs/networkedobjects/lunarshopterminal"; break;
                case TerminalType.Cauldron: ShopString = "prefabs/networkedobjects/lunarcauldron"; break;
                default: ShopString = "prefabs/networkedobjects/multishopterminal"; break;
            }
            GameObject Terminal = Object.Instantiate<GameObject>(Resources.Load<GameObject>(ShopString), transform.GetChild(0).position, transform.GetChild(0).rotation);
            if (!UseCustomIndex)
            {
                Terminal.GetComponent<RoR2.ShopTerminalBehavior>().SetPickupIndex(newPickupIndex, newHidden);
            }
            else
            {
                Terminal.GetComponent<RoR2.ShopTerminalBehavior>().SetPickupIndex(new RoR2.PickupIndex(itemIndex[Random.Range(0, itemIndex.Count)]), newHidden);
            }
            RoR2.PurchaseInteraction purchaseInteraction = Terminal.GetComponent<RoR2.PurchaseInteraction>();
            purchaseInteraction.cost = Price;
            purchaseInteraction.costType = costType;
            NetworkServer.Spawn(Terminal);
        }
        DestroyImmediate(transform.GetChild(0).gameObject);
        DestroyImmediate(this.gameObject);
    }
}
