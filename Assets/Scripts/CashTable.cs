using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashTable : MonoBehaviour
{
    [SerializeField] float distanceActive = 1f;
    [SerializeField] Car car;
    [SerializeField] List<Receiver> receivers;
    [SerializeField] MoneyStore moneyStore;
    bool canSale = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = CharacterCarry.Instance.transform.position - transform.position;
        if (distance.magnitude <= distanceActive)
        {
            Debug.Log("Character is near by");
            canSale = true;
        }
        else
        {
            canSale = false;
        }
        if (canSale == true) {
            if (car.IsWaitingToBuy())
            {
                SellABox();
            }
        }
    }

    void SellABox()
    {
        string tag = car.GetTagRequest();
        for (int i = 0; i < receivers.Count; i++)
        {
            if (tag == receivers[i].GetObjTag() && receivers[i].GetAmountObj() > 0)
            {
                int index = receivers[i].GetIndexWithTag(tag);
                GameObject box = receivers[i].DropObject(index);
                moneyStore.AddMoney(box.GetComponent<FoodBox>().GetPrice());
                Destroy(box);
                car.BuyABox();
                break;
            }
        }
    }
}
