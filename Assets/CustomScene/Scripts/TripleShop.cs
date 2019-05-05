using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class TripleShop : MonoBehaviour
{
    public int BasePrice = 25;
    public RoR2.CostType costType = RoR2.CostType.Money;
    public enum ShopType
    {
        Small,
        Large
    }
    public ShopType shopType;
    private void Awake()
    {
        string ShopString;
        switch(shopType)
        {
            case ShopType.Small: ShopString = "prefabs/networkedobjects/tripleshop"; break;
            case ShopType.Large: ShopString = "prefabs/networkedobjects/tripleshoplarge"; break;
            default: ShopString = "prefabs/networkedobjects/tripleshop"; break;
        }
        if (NetworkServer.active)
        {
            GameObject Shop = Object.Instantiate<GameObject>(Resources.Load<GameObject>(ShopString), transform.GetChild(0).position, transform.GetChild(0).rotation);
            RoR2.MultiShopController controller = Shop.GetComponent<RoR2.MultiShopController>();
            //controller.baseCost = BasePrice;
            controller.costType = costType;
        }
        DestroyImmediate(gameObject);
    }
}
