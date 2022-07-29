using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class YellowBrick : BaseBrick
{
    // POLYMORPHISM
    public override string brickName => "2";
    public override int pointValue => 2;
    public override Color color => Color.yellow;

    void Start()
    {
        SetColor();
    }
}
