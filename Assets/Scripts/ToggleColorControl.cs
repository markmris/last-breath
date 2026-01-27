using UnityEngine;
using UnityEngine.UI;

public class ToggleColorControl : MonoBehaviour
{
    public Image backgroundImage;
    public Toggle toggle;
    public Color enabledColor = Color.green;
    public Color disabledColor = Color.red;

    void Awake()
    {
        toggle = gameObject.GetComponent<Toggle>();
        UpdateColor(toggle.isOn);
        toggle.onValueChanged.AddListener(UpdateColor);
    }

    public void UpdateColor(bool isOn)
    {
        backgroundImage.color = isOn ? enabledColor : disabledColor;
    }
}