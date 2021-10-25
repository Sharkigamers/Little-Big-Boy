using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public float force = 0;

    public override void Use()
    {
        PlayerMovement.instance.TriggerJump(force);
    }
}