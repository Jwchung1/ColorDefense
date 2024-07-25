using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 towerOffset;
    public Vector3 buttonOffset;
    private Renderer rend;
    private Color startColor;
    public GameObject tower;
    private BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    
    void OnMouseDown()
    {
        if(!PlayManager.isGameOver)
        {
            if(buildManager.GetIsNodeSelected() == false)
            {
                // 타워가 이미 있으면
                if(tower != null)
                {
                    buildManager.SelectTower(this);
                }
                // 타워가 없으면
                else
                {
                    buildManager.SelectNode(this);
                }
            }
        }
    }
    void OnMouseEnter()
    {
        if(BuildManager.instance.GetIsNodeSelected() == false)
        {
            rend.material.color = hoverColor;
        }
        
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && tower != null)
        {
            TowerInfo.instance.ShowTowerInfo(tower);
        }
    }
    void OnMouseExit()
    {
        if(BuildManager.instance.GetIsNodeSelected() == false)
            rend.material.color = startColor;
    }
    public void DestroyTower()
    {
        Destroy(tower);
        tower = null;
    }
    public void BuildTower(GameObject towerToBuild)
    {
        GameObject towerBuilt = (GameObject)Instantiate(towerToBuild, transform.position + towerOffset, towerToBuild.transform.rotation);
        tower = towerBuilt;
    }
    public bool isTowerInPallete;
    public string GetTowerColor()
    {
        return tower.GetComponent<Tower>().towerColor;
    }

}
