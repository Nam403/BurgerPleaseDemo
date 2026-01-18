using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePoint : MonoBehaviour
{
    [SerializeField] int upgradeMoneyAmount = 100;
    [SerializeField] int upgradeMoneyUnit = 10;
    [SerializeField] float distanceUpgrade = 1f;
    [SerializeField] float countDownUpgrade = 0.1f;
    [SerializeField] List<GameObject> activeObjs = new List<GameObject>();
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Image process;
    public static event Action Upgrade;
    public static event Action CarCanMove;
    int getMoneyAmount = 0;
    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        countDown = countDownUpgrade; 
        moneyText.text = upgradeMoneyAmount.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = CharacterCarry.Instance.transform.position - transform.position;
        if (distance.magnitude <= distanceUpgrade)
        {
            if (countDown <= 0)
            {
                if (getMoneyAmount < upgradeMoneyAmount && TradeManager.Instance.GetMoneyRemain() >= upgradeMoneyUnit)
                {
                    getMoneyAmount += upgradeMoneyUnit;
                    TradeManager.Instance.UpdateMoney(-upgradeMoneyUnit);
                }
                countDown = countDownUpgrade;
            }
            else
            {
                countDown -= Time.deltaTime;
            }
        }
        else
        {
            countDown = countDownUpgrade;
        }
        process.fillAmount = (1f * getMoneyAmount) / (1f * upgradeMoneyAmount);
        if(getMoneyAmount == upgradeMoneyAmount)
        {
            ActiveAllObjs();
            Upgrade.Invoke();
            this.gameObject.SetActive(false);   
        }
    }

    void ActiveAllObjs()
    {
        for(int i = 0; i < activeObjs.Count; i++)
        {
            activeObjs[i].SetActive(true);
        }
        if (activeObjs[0].GetComponent<CashTable>() != null) 
        {
            CarCanMove.Invoke();
        }
        if(activeObjs.Count == 2)  // Enable for Coca
        {
            TradeManager.Instance.AddBoxTag("CocaBox");
        }
    }

    public int GetUpgradeMoney() 
    { 
        return upgradeMoneyAmount; 
    }
}
