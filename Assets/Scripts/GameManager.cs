using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<UpgradePoint> upgradePoints = new List<UpgradePoint>();
    [SerializeField] GameObject UIEndGame;
    int currentPointIndex = 0;
    void OnEnable()
    {
        UpgradePoint.Upgrade += HandleUpgrade;
    }
    void OnDisable()
    {
        UpgradePoint.Upgrade -= HandleUpgrade;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(upgradePoints.Count > 0)
        {
            upgradePoints[0].gameObject.SetActive(true);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPointIndex < upgradePoints.Count)
        {
            EnableUpgradePoint();
        }
    }

    void HandleUpgrade()
    {
        currentPointIndex++;
        if (currentPointIndex < upgradePoints.Count)
        {
            EnableUpgradePoint();
        }
        else
        {
            UIEndGame.SetActive(true);
        }
    }

    void EnableUpgradePoint()
    {
        if (!upgradePoints[currentPointIndex].gameObject)
        {
            return;
        }
        if (TradeManager.Instance.GetMoneyRemain() > upgradePoints[currentPointIndex].GetUpgradeMoney() && !upgradePoints[currentPointIndex].gameObject.activeSelf)
        {
            upgradePoints[currentPointIndex].gameObject.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
