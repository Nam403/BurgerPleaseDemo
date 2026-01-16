using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCarry : MonoBehaviour
{
    public static CharacterCarry Instance;
    [SerializeField] GameObject container;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanCarry()
    {
        return container.GetComponent<CarriedObjContainer>().IsFull() == false;
    }
    public bool HaveObjWithTag(string tag)
    {
        int findIndex = container.GetComponent<CarriedObjContainer>().GetIndexWithTag(tag);
        if (findIndex == -1)
            return false;
        else return true;
    }

    public void GetCarriedObject(GameObject obj)
    {
        if (!CanCarry())
        {
            return;
        }
        container.GetComponent<CarriedObjContainer>().GetObject(obj);
    }

    public GameObject DropCarriedObject(string tag)
    {
        int findIndex = container.GetComponent<CarriedObjContainer>().GetIndexWithTag(tag);
        if (findIndex == -1)
        {
            return null;
        }
        GameObject obj = container.GetComponent<CarriedObjContainer>().DropObject(findIndex);
        obj.transform.parent = null;
        return obj;
    }
}
