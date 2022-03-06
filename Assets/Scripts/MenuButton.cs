using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Image rightIndicator;
    public TextMeshProUGUI text;

    public void ShowIndicators()
    {
        rightIndicator.enabled = true;
        text.fontSize *= 1.15f;
    }

    public void HideIndicators()
    {
        rightIndicator.enabled = false;
        text.fontSize /= 1.15f;
    }
}
