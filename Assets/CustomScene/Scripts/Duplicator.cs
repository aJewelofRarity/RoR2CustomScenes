using UnityEngine;
using UnityEngine.Networking;

public class Duplicator : MonoBehaviour
{
    public int Price = 1;
    public RoR2.CostType costType;
    public enum PrinterType
    {
        Small,
        Large,
        Military
    }
    public PrinterType printerType;
    private void Awake()
    {
        if (NetworkServer.active)
        {
            string prefab;
            switch (printerType)
            {
                case PrinterType.Small: prefab = "prefabs/networkedobjects/duplicator"; break;
                case PrinterType.Military: prefab = "prefabs/networkedobjects/duplicatormilitary"; break;
                case PrinterType.Large: prefab = "prefabs/networkedobjects/duplicatorlarge"; break;
                default: prefab = "prefabs/networkedobjects/duplicator"; break;
            }
            GameObject Printer = Object.Instantiate<GameObject>(Resources.Load<GameObject>(prefab), transform.GetChild(0).position, transform.GetChild(0).rotation);
            RoR2.PurchaseInteraction purchaseInteraction = Printer.GetComponent<RoR2.PurchaseInteraction>();
            RoR2.ShopTerminalBehavior shopTerminal = Printer.GetComponent<RoR2.ShopTerminalBehavior>();
            purchaseInteraction.cost = Price;
            purchaseInteraction.costType = costType;
            NetworkServer.Spawn(Printer);
        }
        DestroyImmediate(transform.GetChild(0).gameObject);
        DestroyImmediate(this.gameObject);
    }
}
