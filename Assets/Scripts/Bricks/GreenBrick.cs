using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBrick : BaseBrick
{
    public override string brickName => "1";
    public override int pointValue => 1;
    public override Color color => Color.green;

    void Start()
    {
        SetColor();
    }
}
