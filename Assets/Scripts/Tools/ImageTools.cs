using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageTools {

    public static void SetOpacity(float percentage, Image original)
    {
        Color color = original.color;
        color.a = percentage / 100;
        original.color = color;
    }



}
