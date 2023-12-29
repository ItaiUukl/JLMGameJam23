using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePod : MonoBehaviour
{
    [SerializeField] private Globals.LanesEnum lane;
    private Globals.ColorsEnum _color;
    
    public Globals.ColorsEnum color
    {
        get { return _color; }   // get method
        set { _color = value; }  // set method
    }

    public Vector3 GetPositionForPlayer() {
        return transform.position;
    }
    
    public void SetColor(Globals.ColorsEnum color) {
        _color = color;
    }

    public void ResetColor() {
        _color = Globals.ColorsEnum.Clear;
    }

    public void AddColor(Globals.ColorsEnum color) {
        SetColor(Globals.GetMixedColor(_color, color));
    }
}
