using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    [SerializeField] TMP_Text textValue;
    [SerializeField] Slider valueSlider;

    float currentValue;

    public void SetupValues(float maxValue)
    {
        currentValue = maxValue;
        if(valueSlider)
            valueSlider.maxValue = maxValue;

        DisplayValues();
    }

    public void RefreshValues(float newValue)
    {
        currentValue = newValue;
        DisplayValues();
    }

    void DisplayValues()
    {
        if(textValue)
            textValue.text = currentValue.ToString() + "/" + valueSlider.maxValue.ToString();

        if(valueSlider)
            valueSlider.value = currentValue;
    }
}
