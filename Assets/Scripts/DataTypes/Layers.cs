using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
    // It correspond to LayerID
    // e.g. (int)Layers.Name.Water == 4
    // e.g. Layers.Name.Water.ToString() == "Water"

    public enum Name
    {
        Water = 4,
        UI = 5,
        Animals = 6,
        Plants = 7,
        Ground = 8,
    }
}
