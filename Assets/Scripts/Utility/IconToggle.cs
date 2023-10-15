using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconToggle : MonoBehaviour
{
    public Sprite iconEnable;
    public Sprite iconDisable;
    public bool defaultIconEnable = true;

    Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = defaultIconEnable ? iconEnable : iconDisable;
    }
    public void ToggleIcon(bool state)
    {
        if(!image || !iconEnable || !iconDisable)
        {
            return;
        }
        image.sprite = state ? iconEnable : iconDisable;
    }
}
