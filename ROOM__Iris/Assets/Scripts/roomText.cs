using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class roomText : MonoBehaviour
{
    Material material;
    byte alpha;
    bool glowUP;

    // Start is called before the first frame update
    void Start()
    {
        material = this.GetComponent<Material>();
        alpha = 1;
        glowUP = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha >= 255)
        {
            glowUP = false;
        }

        if (alpha <= 0)
        {
            glowUP = true;
        }
        if(alpha < 255 && glowUP)
        {
            alpha += 1;
        }

        else if (alpha > 0 && !glowUP)
        {
            alpha -= 1;
        }
        material.SetColor(ShaderUtilities.ID_GlowColor, new Color32(0, 255, 0, alpha));
    }
}
