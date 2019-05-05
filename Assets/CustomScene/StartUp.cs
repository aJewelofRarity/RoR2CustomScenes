using MultiMod.Interface;
using UnityEngine;
using UnityEngine.UI;

public class StartUp : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("StartUp Script Awoken");
    }
    private void Start()
    {
        Debug.Log("StartUp Script Started");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RoR2.Run.instance.AdvanceStage("CustomScene");
        }
    }
}
