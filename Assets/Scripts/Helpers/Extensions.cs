using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class Extensions
{
    // POLYMORPHISM
    public static Component AddBrickComponent(this GameObject gameObject, System.Type aType)
    {
        var brick = gameObject.AddComponent(aType);
        if (brick != null)
            return brick;

        return null;
    }

    // POLYMORPHISM
    public static T AddBrickComponent<T>(this GameObject gameObject) where T : BaseBrick
    {
        BaseBrick brick = gameObject.AddComponent<T>();
        if (brick != null)
            return brick as T;

        return null;
    }

    public static T GetBrickComponent<T>(this GameObject gameObject) where T : BaseBrick
    {
        BaseBrick brick = gameObject.GetComponent<BaseBrick>();
        if (brick != null)
            return brick as T;

        return null;
    }
}
