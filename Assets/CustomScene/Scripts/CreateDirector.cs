using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateDirector : MonoBehaviour
{
    private void Awake()
    {
        if (NetworkServer.active)
        {
            gameObject.AddComponent<RoR2.DirectorCore>();
            gameObject.AddComponent<RoR2.SceneDirector>();
            gameObject.AddComponent<RoR2.CombatDirector>();
            gameObject.AddComponent<RoR2.CombatDirector>();
        }
        Destroy(this.gameObject.GetComponent<CreateDirector>());
    }
}
