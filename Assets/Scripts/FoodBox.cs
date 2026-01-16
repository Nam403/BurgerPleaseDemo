using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBox : CarriedObject
{
    [SerializeField] int price = 10; // Price of the food box

    public int GetPrice()
    {
        return price;
    }
}
