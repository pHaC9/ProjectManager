using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static List<Item> items;
    // Start is called before the first frame update
    void Start()
    {
        items = new List<Item>();
        items.Capacity = 1;
    }
}
