using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBrick : BaseBrick
{
    public override string brickName => "2";
    public override int pointValue => 2;
    public override Color color => Color.yellow;

    void Start()
    {
        SetColor();
    }
}
