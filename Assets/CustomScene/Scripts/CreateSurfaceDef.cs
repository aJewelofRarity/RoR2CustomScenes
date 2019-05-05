using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateSurfaceDef : MonoBehaviour
{
    SurfaceDef surface = new SurfaceDef();
    private void Awake()
    {
        if (NetworkServer.active)
        {
            SurfaceDefProvider Provider = gameObject.AddComponent<SurfaceDefProvider>();
            Provider.surfaceDef = surface;
        }
        Destroy(gameObject.GetComponent<CreateSurfaceDef>());
    }
}
