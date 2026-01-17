using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    public static TradeManager Instance;
    List<string> foodBoxTag = new List<string> { "BurgerBox"};
    int moneyCount;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        moneyCount = 50;
        moneyText.text = moneyCount.ToString() + "$";
    }
    public string GetFoodBoxTag(int index)
    {
        if (index < foodBoxTag.Count)
        {
            return foodBoxTag[index];
        }
        return "";
    }
    public int GetSizeOfBoxTagList()
    {
        {
            return foodBoxTag.Count;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
    public void UpdateMoney(int money)
    {
        if(moneyCount + money >= 0)
        {
            moneyCount += money;
            moneyText.text = moneyCount.ToString() + "$";
        }   
    }
    public int GetMoneyRemain()
    {
        return moneyCount;
    }

    public void AddBoxTag(string boxTag)
    {
        foodBoxTag.Add(boxTag);
    }   
}