using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriedObjContainer : MonoBehaviour
{
    [SerializeField] Vector3 rotateVector = new Vector3(0f, 0f, 0f);
    [SerializeField] int sizeLimitY = 8;
    [SerializeField] int sizeBlockX = 1;
    [SerializeField] int sizeBlockZ = 1;
    int listSizeLimit = 8;
    Vector3 rootPosition;
    List<GameObject> carriedObjects = new List<GameObject>();
    protected void Start()
    {
        listSizeLimit = sizeLimitY * sizeBlockX * sizeBlockZ;
    }
    public void GetObject(GameObject obj)
    {
        if (carriedObjects.Count == listSizeLimit)
        {
            return;
        }
        obj.transform.parent = transform;
        UpdateLocalPosition(obj, carriedObjects.Count);
        obj.transform.localEulerAngles = rotateVector;
        carriedObjects.Add(obj);
    }

    public GameObject DropObject(int index)
    {
        if (index < 0 && index >= carriedObjects.Count)
        {
            return null;
        }
        GameObject obj = carriedObjects[index];
        carriedObjects.RemoveAt(index);
        obj.transform.parent = null;
        if (index < carriedObjects.Count)
        {
            for (int i = index; i < carriedObjects.Count; i++)
            {
                UpdateLocalPosition(carriedObjects[i], i);
            }
        }
        return obj;
    }
    public int GetIndexWithTag(string tag)
    {
        for (int i = carriedObjects.Count - 1; i >= 0; i--)
        {
            if (carriedObjects[i].tag == tag)
            {
                return i;
            }
        }
        return -1;
    }
    public void UpdateLocalPosition(GameObject obj, int index)
    {
        CarriedObject carriedObj = obj.GetComponent<CarriedObject>();
        rootPosition = new Vector3(-((1f * sizeBlockX) / 2f - 0.5f) * carriedObj.GetDistanceX(), 0, -((1f * sizeBlockZ) /2f - 0.5f) * carriedObj.GetDistanceZ());
        int yIndex = index / (sizeBlockX * sizeBlockZ);
        int xIndex = (index / sizeBlockZ) % sizeBlockX;
        int zIndex = index % sizeBlockZ;
        Debug.Log("Local position: " + xIndex + ", " + yIndex + ", " + zIndex);
        float posX = xIndex * carriedObj.GetDistanceX() / transform.parent.localScale.x;
        float posY = yIndex * carriedObj.GetDistanceY() / transform.parent.localScale.y;
        float posZ = zIndex * carriedObj.GetDistanceZ() / transform.parent.localScale.z;
        obj.transform.localPosition = rootPosition + new Vector3(posX, posY, posZ);
    }
    public bool IsFull()
    {
        return carriedObjects.Count >= listSizeLimit;
    }
    public int GetAmountObj()
    {
        return carriedObjects.Count;
    }
}
