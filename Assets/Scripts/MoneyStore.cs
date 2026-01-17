using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStore : CarriedObjContainer
{
    [SerializeField] int moneyPerPrefab = 10;
    [SerializeField] GameObject moneyPrefab;
    [SerializeField] string tabObj = "Money";
    [SerializeField] float distanceToGetMoney = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = CharacterCarry.Instance.transform.position - transform.position;
        if (distance.magnitude <= distanceToGetMoney)
        {
            if(GetAmountObj() > 0)
            {
                int index = GetIndexWithTag(tabObj);
                GameObject moneyObj = DropObject(index);
                TradeManager.Instance.UpdateMoney(moneyPerPrefab);
                Destroy(moneyObj);
            }
        }
        else
        {
            
        }
    }
    public void AddMoney(int money)
    {
        int prefabCount = money / moneyPerPrefab;
        for (int i = 0; i < prefabCount; i++) 
        {
            GameObject moneyObj = Instantiate(moneyPrefab);
            GetObject(moneyObj);
        }
    }
}
