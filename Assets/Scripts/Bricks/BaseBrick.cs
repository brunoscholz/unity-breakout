using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// INHERITANCE
public class BaseBrick : MonoBehaviour
{
    // POLYMORPHISM and ENCAPSULATION
    public virtual string brickName { get; }
    public virtual int pointValue { get; }
    public virtual Color color { get { return Color.red; } }
    public event Action<int> onDestroyed = delegate { };

    protected void SetColor()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", color);
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
