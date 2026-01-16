using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : CarriedObject
{
    [SerializeField] int amountForBox = 1; // Food amount for a food box (burger of coca)

    public int GetAmountForBox()
    {
        return amountForBox;
    }
}
