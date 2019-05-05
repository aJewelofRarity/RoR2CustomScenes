using UnityEngine;
using UnityEngine.Networking;

public class Portal : MonoBehaviour
{
    public enum PortalType
    {
        MS,
        GoldShore,
        Shop
    }
    public PortalType portalType;
    public bool IsDefault = true;
    public string DestinationScene;
    public string InteractionMessage = null;
    private void Awake()
    {
        if (NetworkServer.active)
        {
            string PortalString;
            switch (portalType)
            {
                case PortalType.MS: PortalString = "prefabs/networkedobjects/portalms"; break;
                case PortalType.Shop: PortalString = "prefabs/networkedobjects/portalshop"; break;
                case PortalType.GoldShore: PortalString = "prefabs/networkedobjects/portalgoldshores"; break;
                default: PortalString = "prefabs/networkedobjects/portalms"; break;
            }
            GameObject Portal = Object.Instantiate<GameObject>(Resources.Load<GameObject>(PortalString), transform.GetChild(0).position, transform.GetChild(0).rotation);
            if (!IsDefault)
            {
                RoR2.SceneExitController Exit = Portal.GetComponent<RoR2.SceneExitController>();
                if (InteractionMessage != null)
                {
                    RoR2.GenericInteraction Interaction = Portal.GetComponent<RoR2.GenericInteraction>();
                    Interaction.contextToken = InteractionMessage;
                }
                GameObject Quad = Portal.gameObject.transform.Find("PortalCenter").gameObject.transform.Find("Quad").gameObject;
                MeshRenderer Render = Quad.GetComponent<MeshRenderer>();
                Render.material.shader = Shader.Find("Hopoo/Cutout/Opaque Cloud Remap");
                Exit.destinationScene = new SceneField(DestinationScene);
            }
            NetworkServer.Spawn(Portal);
        }
        DestroyImmediate(transform.GetChild(0).gameObject);
        DestroyImmediate(this.gameObject);
    }
}
