using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Globals.ColorsEnum _color;

    
    public Globals.ColorsEnum color
    {
        get { return _color; }   // get method
        set { _color = value; }  // set method
    }
}
