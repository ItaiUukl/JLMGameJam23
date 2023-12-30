using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Globals.ColorsEnum _color;
    private Lane _lane;

    public int Life = 3;
    [SerializeField] private TextMeshProUGUI LifeText;

    public Globals.ColorsEnum color
    {
        get { return _color; }   // get method
        set { _color = value; }  // set method
    }
    
    public Lane lane
    {
        get { return _lane; }   // get method
        set { _lane = value; }  // set method
    }

    public void GetDamage()
    {
        Life -= 1;
        LifeText.text = Life.ToString();
    }
}
