using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// INHERITANCE
public class MetalBrick : BaseBrick
{
    // POLYMORPHISM
    public override string brickName => "0";
    public override int pointValue => 0;
    public override Color color => new Color(0.1737368f, 0.1573959f, 0.254717f, 1f);

    void Start()
    {
        SetColor();
    }

}
