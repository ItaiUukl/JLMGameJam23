using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Globals : ScriptableObject
{
    public enum ColorsEnum
    {
        Clear = -1,
        Blue = 0,
        Red = 1,
        Yellow = 2,
        Purple = 3,
        Green = 4,
        Orange = 5
    }

    public enum LanesEnum
    {
        Lane1 = 0,
        Lane2 = 1,
        Lane3 = 2
    }
    
    public static ColorsEnum GetMixedColor(ColorsEnum color1, ColorsEnum color2)
    {
        if ((int)color1 > 2 || (int)color2 > 2) return ColorsEnum.Clear;

        return (ColorsEnum)((int)color1 + (int)color2 + 3);
    }

}
