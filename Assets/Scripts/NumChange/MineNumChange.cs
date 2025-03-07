using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(Text))]
public class MineNumChange : MonoBehaviour
{
    public void Start()
    {
        EventManager.Instance.AddListener<MineNumChangeEventArgs>(EventType.MineNumChange, OnMineNumChange);
    }
    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<MineNumChangeEventArgs>(EventType.MineNumChange, OnMineNumChange);
    }
    public void OnMineNumChange(MineNumChangeEventArgs args)
    {
        this.GetComponent<Text>().text = args.countMine.ToString()+"/"+args.AllMine.ToString();
    }
}
