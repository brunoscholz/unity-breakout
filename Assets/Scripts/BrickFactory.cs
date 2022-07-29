using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFactory
{
    private Dictionary<string, Type> bricksByName;

    public BrickFactory()
    {
        // var brickTypes = Assembly.GetAssembly(typeof(BaseBrick)).GetTypes()
        //   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BaseBrick)));

        bricksByName = new Dictionary<string, Type>();

        // foreach (var type in brickTypes)
        // {
        //     var tempEffect = Activator.CreateInstance(type) as BaseBrick;
        //     bricksByName.Add(tempEffect.brickName, type);
        // }

        // bricksByName.Add("0", typeof(MetalBrick));
        // bricksByName.Add("1", typeof(GreenBrick));
        // bricksByName.Add("2", typeof(YellowBrick));
        // bricksByName.Add("5", typeof(BlueBrick));
    }

    public BaseBrick GetBrick(string brickType, GameObject prefab)
    {
        // if (bricksByName.ContainsKey(brickType))
        // {
        //     Type type = bricksByName[brickType];
        //     // var brick = Activator.CreateInstance(type) as BaseBrick;
        //     var brick = prefab.AddBrickComponent(type);
        //     return brick.GetBrickComponent<BaseBrick>();
        // }

        // return null;
        switch (brickType)
        {
            case "0":
                // return new MetalBrick();
                return prefab.AddBrickComponent<MetalBrick>();
            case "1":
                // return new Brick(); // green
                return prefab.AddBrickComponent<GreenBrick>();
            case "2":
                // return new Brick(); // yellow
                return prefab.AddBrickComponent<YellowBrick>();
            case "5":
                // return new Brick(); // blue
                return prefab.AddBrickComponent<BlueBrick>();
            case "D":
                // return new Brick(); // glass
            case "F":
                // return new Brick(); // fire
            case "X":
            default:
                return null;
        }
    }

    internal IEnumerable<string> GetBrickNames()
    {
        return bricksByName.Keys;
    }
}