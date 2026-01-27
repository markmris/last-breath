using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameModControl : MonoBehaviour
{
    public Dictionary<string, bool> mods = new();
    public Transform modsContainer;

    void Awake()
    {
        modsContainer = GameObject.Find("Mods").transform;

        foreach (Transform mod in modsContainer)
        {
            mods.Add(mod.name, mod.GetComponent<Toggle>().isOn);
        }

        Destroy(modsContainer.gameObject);
    }
    
}