using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashTable : MonoBehaviour
{
    [SerializeField] float distanceActive = 1f;
    [SerializeField] Car car;
    [SerializeField] List<Receiver> receivers;
    [SerializeField] MoneyStore moneyStore;
    [SerializeField] float countDownSellTime = 0.25f;
    bool canSale = false;
    float countDownSell;
    // Start is called before the first frame update
    void Start()
    {
        countDownSell = countDownSellTime;
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
                if (countDownSell <= 0) {
                    SellABox();
                    countDownSell = countDownSellTime;
                }
                else
                {
                    countDownSell -= Time.deltaTime;
                }
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
