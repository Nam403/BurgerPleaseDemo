using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : CarriedObjContainer
{
    [SerializeField] float distanceReceiver = 0.5f;
    [SerializeField] float countDownTime = 0.25f;
    [SerializeField] string objTag = "BurgerBox";
    float countDownRec;
    // Start is called before the first frame update
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
                DistributeBox();
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
    void DistributeBox()
    {
        if (!CharacterCarry.Instance.CanCarry())
        {
            return;
        }
        if(GetAmountObj() == 0)
        {
            return;
        }
        int index = GetIndexWithTag(objTag);
        GameObject box = DropObject(index);
        CharacterCarry.Instance.GetCarriedObject(box);
    }
}
