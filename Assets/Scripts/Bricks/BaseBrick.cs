using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBrick : MonoBehaviour
{
    // public UnityEvent<int> onDestroyed;

    public virtual string brickName { get; }
    public virtual int pointValue { get; }
    public virtual Color color { get { return Color.red; } }
    public event Action<int> onDestroyed = delegate { };

    void Start()
    {
        // SetColor();
    }

    protected void SetColor()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", color);
        // switch (PointValue)
        // {
        //     case 1 :
        //         block.SetColor("_BaseColor", Color.green);
        //         break;
        //     case 2:
        //         block.SetColor("_BaseColor", Color.yellow);
        //         break;
        //     case 5:
        //         block.SetColor("_BaseColor", Color.blue);
        //         break;
        //     default:
        //         block.SetColor("_BaseColor", new Color(25, 64, 128));
        //         break;
        // }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (pointValue != 0) {
            // onDestroyed.Invoke(pointValue);
            onDestroyed(pointValue);
            //slight delay to be sure the ball have time to bounce
            Destroy(gameObject, 0.2f);
        }
    }
}
