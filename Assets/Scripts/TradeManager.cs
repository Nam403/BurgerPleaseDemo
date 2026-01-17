using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    public static TradeManager Instance;
    List<string> foodBoxTag = new List<string> { "BurgerBox", "CocaBox" };
    int moneyCount = 0;
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
        moneyCount += money;
        moneyText.text = moneyCount.ToString() + "$";
    }
}