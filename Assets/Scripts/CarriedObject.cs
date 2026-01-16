using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriedObject : MonoBehaviour
{
    [SerializeField] float distanceX = 0.2f; // Distance between object with X
    [SerializeField] float distanceY = 0.2f; // Distance between object with Y
    [SerializeField] float distanceZ = 0.2f; // Distance between object with Z
    public float GetDistanceX()
    {
        return distanceX;
    }
    public float GetDistanceY()
    {
        return distanceY;
    }
    public float GetDistanceZ()
    {
        return distanceZ;
    }
}
