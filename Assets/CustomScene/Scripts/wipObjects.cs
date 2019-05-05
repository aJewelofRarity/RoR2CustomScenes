using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class wipObjects : MonoBehaviour
{
    public string PrefabString;
    private void Awake()
    {
        Instantiate<GameObject>(Resources.Load<GameObject>(PrefabString), transform.position, transform.rotation);
    }
}
