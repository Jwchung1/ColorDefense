using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.transform.position;
        ui.SetActive(true);
    }
    public void Hide()
    {
        ui.SetActive(false);
    }
}
