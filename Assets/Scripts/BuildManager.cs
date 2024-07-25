using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public NodeUI nodeUI;
    public TowerUI towerUI;
    public Vector3 buildOffset;
    private Node selectedNode = null;
    private bool isNodeSelected = false;
    void Awake()
    {
        instance = this;
    }
    public GameObject[] basicTower;

    public GameObject GetRandomTower()
    {
        int randomIndex = Random.Range(0,basicTower.Length);
        return basicTower[randomIndex];
    }
    public void SelectNode(Node node)
    {
        selectedNode = node;
        nodeUI.SetTarget(node);
        isNodeSelected = true;
    }
    public void SelectTower(Node node)
    {
        selectedNode = node;
        towerUI.SetTarget(node);
        isNodeSelected = true;
    }
    // build 버튼 눌렀을때
    public void BuildTower()
    {
        if(PlayManager.instance.CanBuy(20))
        {
            PlayManager.instance.UseGold(20);
            GameObject towerToBuild = GetRandomTower();
            GameObject towerBuilt = (GameObject)Instantiate(towerToBuild, selectedNode.transform.position + buildOffset, towerToBuild.transform.rotation);
            selectedNode.tower = towerBuilt;
        }
        else
        {
            Debug.Log("not enough gold");
        }
        CancelNodeUI();
    }

    // cancle 버튼 눌렀을때
    public void CancelNodeUI()
    {
        nodeUI.Hide();
        isNodeSelected = false;
        selectedNode = null;
    }
    public void CancelTowerUI()
    {
        towerUI.Hide();
        isNodeSelected = false;
        selectedNode = null;
    }
    public bool GetIsNodeSelected()
    {
        return isNodeSelected;
    }
    // sell 버튼 눌렀을때
    public void SellTower()
    {
        selectedNode.DestroyTower();
        PlayManager.instance.EarnGold(10);
        CancelTowerUI();
    }
}
