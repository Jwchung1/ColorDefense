using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    [SerializeField] Vector3 offset;
    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.transform.position + offset;
        ui.SetActive(true);
    }
    public void Hide()
    {
        ui.SetActive(false);
    }
}
