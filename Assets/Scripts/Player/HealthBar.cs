using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite heart;
    public Sprite brokenHeart;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image armorHeart1;
    public Image armorHeart2;

    public void SetHealth(int health)
    {
        switch (health)
        {
            case (0):
                heart1.sprite = brokenHeart;
                heart2.sprite = brokenHeart;
                heart3.sprite = brokenHeart;
                armorHeart1.enabled = false;
                armorHeart2.enabled = false;
                break;
            case (1):
                heart1.sprite = heart;
                heart2.sprite = brokenHeart;
                heart3.sprite = brokenHeart;
                armorHeart1.enabled = false;
                armorHeart2.enabled = false;
                break;
            case (2):
                heart1.sprite = heart;
                heart2.sprite = heart;
                heart3.sprite = brokenHeart;
                armorHeart1.enabled = false;
                armorHeart2.enabled = false;
                break;
            case (3):
                heart1.sprite = heart;
                heart2.sprite = heart;
                heart3.sprite = heart;
                armorHeart1.enabled = false;
                armorHeart2.enabled = false;
                break;
            case (4):
                heart1.sprite = heart;
                heart2.sprite = heart;
                heart3.sprite = heart;
                armorHeart1.enabled = true;
                armorHeart2.enabled = false;
                break;
            case (5):
                heart1.sprite = heart;
                heart2.sprite = heart;
                heart3.sprite = heart;
                armorHeart1.enabled = true;
                armorHeart2.enabled = true;
                break;
        }
    }
}
