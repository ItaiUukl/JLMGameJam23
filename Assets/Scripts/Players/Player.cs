using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Globals.ColorsEnum _color;
    private Globals.LanesEnum _lane;
    
    public Globals.ColorsEnum color
    {
        get { return _color; }   // get method
        set { _color = value; }  // set method
    }
    
    public Globals.LanesEnum lane
    {
        get { return _lane; }   // get method
        set { _lane = value; }  // set method
    }
}
