using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePoint : MonoBehaviour
{
    [SerializeField] int upgradeMoneyAmount = 100;
    [SerializeField] int upgradeMoneyUnit = 10;
    [SerializeField] float distanceUpgrade = 1f;
    [SerializeField] List<GameObject> activeObjs = new List<GameObject>();
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Image process;
    int getMoneyAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = upgradeMoneyAmount.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = CharacterCarry.Instance.transform.position - transform.position;
        if (distance.magnitude <= distanceUpgrade && getMoneyAmount < upgradeMoneyAmount && TradeManager.Instance.GetMoneyRemain() >= upgradeMoneyUnit)
        {
            getMoneyAmount += upgradeMoneyUnit;
            TradeManager.Instance.UpdateMoney(-upgradeMoneyUnit);
        }
        process.fillAmount = (1f * getMoneyAmount) / (1f * upgradeMoneyAmount);
        if(getMoneyAmount == upgradeMoneyAmount)
        {
            ActiveAllObjs();
            this.gameObject.SetActive(false);   
        }
    }

    void ActiveAllObjs()
    {
        for(int i = 0; i < activeObjs.Count; i++)
        {
            activeObjs[i].SetActive(true);
        }
    }
}
