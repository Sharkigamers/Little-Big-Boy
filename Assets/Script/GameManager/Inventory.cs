﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one Inventory of instance found!");
            return;
        }
        instance = this;
    }

    #endregion

    private int space = 4;

    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if (!item.isDefaultItem)
            if (items.Count < space) {
                items.Add(item);
                return true;
            }
            return false;
    }

    public void Remove(Item item) {
        items.Remove(item);
    }
}
