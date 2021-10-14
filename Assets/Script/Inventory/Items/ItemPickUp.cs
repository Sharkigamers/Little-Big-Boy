using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    void PickUp() {
        Inventory.instance.Add(item);

        Destroy(gameObject);
    }
}
