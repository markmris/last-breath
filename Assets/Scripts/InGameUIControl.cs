using UnityEngine.UI;
using UnityEngine;

public class InGameUIControl : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Image[] staminaIcons = new Image[5];

    public void ReduceStamina(int i)
    {
        Color color = staminaIcons[i].color;
        color.a = 0.4f;
        staminaIcons[i].color = color;
    }

    public void AddStamina(int i)
    {
        Color color = staminaIcons[i].color;
        color.a = 1;
        staminaIcons[i].color = color;
    }

}
