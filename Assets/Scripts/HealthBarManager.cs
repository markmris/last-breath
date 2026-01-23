using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Image meter;

    public void ChangeHealth(float health)
    {
        meter.fillAmount = Math.Clamp(health, 0, 100) / 100;
        Debug.Log("HEALTH METER CHANGED");
    }
}
