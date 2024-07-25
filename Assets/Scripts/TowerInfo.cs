using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TowerInfo : MonoBehaviour
{
    public static TowerInfo instance;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI fireRateText;
    void Awake()
    {
        instance = this;
    }
    public void ShowTowerInfo(GameObject tower)
    {
        Tower towerInfo = tower.GetComponent<Tower>();
        nameText.text = towerInfo.towerColor;
        rangeText.text = "Range: " + towerInfo.range.ToString();
        damageText.text = "Damage: " + towerInfo.damage.ToString();
        fireRateText.text = "FireRate: " + towerInfo.fireRate.ToString();
    }
}
