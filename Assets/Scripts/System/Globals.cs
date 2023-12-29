using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Globals : ScriptableObject
{
    public enum ColorsEnum
    {
        Clear = 1,
        Blue = 2,
        Red = 3,
        Yellow = 5,
        Purple = 6,
        Green = 10,
        Orange = 15,
        Brown = 30
    }

    public enum LanesEnum
    {
        Lane1 = 0,
        Lane2 = 1,
        Lane3 = 2
    }
    
    public static ColorsEnum GetMixedColor(ColorsEnum color1, ColorsEnum color2)
    {
        var minColorValue = Math.Min((int)color1, (int)color2);
        var maxColorValue = Math.Max((int)color1, (int)color2);

        if (maxColorValue % minColorValue == 0) return (ColorsEnum)maxColorValue;
        return (ColorsEnum)(maxColorValue * minColorValue);
    }
    
    
    
    public static LayerMask PlayerLayer = LayerMask.NameToLayer("Player");
    public static LayerMask EnemyLayer = LayerMask.NameToLayer("Enemy");
    public static LayerMask PodLayer = LayerMask.NameToLayer("Pod");

}
