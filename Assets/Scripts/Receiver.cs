using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : CarriedObjContainer
{
    [SerializeField] float distanceReceiver = 0.5f;
    [SerializeField] float countDownTime = 0.25f;
    [SerializeField] string objTag = "Burger"; 
    float countDownRec;
    void Start()
    {
        base.Start();
        countDownRec = countDownTime;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 distance = CharacterCarry.Instance.transform.position - transform.position;
        if (distance.magnitude <= distanceReceiver)
        {
            Debug.Log("Character is near by");
            if (countDownRec <= 0)
            {
                GetFood();
                countDownRec = countDownTime;
            }
            else
            {
                countDownRec -= Time.deltaTime;
            }
        }
        else
        {
            countDownRec = countDownTime;
        }
    }

    void GetFood()
    {
        if (CharacterCarry.Instance.HaveObjWithTag(objTag) == false)
        {
            return;
        }
        if (IsFull())
        {
            return;
        }
        GameObject food = CharacterCarry.Instance.DropCarriedObject(objTag);
        GetObject(food);
    }
    public string GetObjTag()
    {
        return objTag;
    }
}
