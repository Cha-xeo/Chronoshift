using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fire1 : Spells
{
    Light2D Torch;

    public override void Use()
    {
        base.Use();
        Instantiate(this, InputManager.GetInstance().GetMousePosition(), Quaternion.identity);
    }
}
