using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Items
{
    public GameObject obj;
    [Range(0, 1)]
    public float dropPercentage;
}
