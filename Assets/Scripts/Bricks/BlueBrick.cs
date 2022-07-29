using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BlueBrick : BaseBrick
{
    // POLYMORPHISM
    public override string brickName => "5";
    public override int pointValue => 5;
    public override Color color => Color.blue;

    void Start()
    {
        SetColor();
    }
}
