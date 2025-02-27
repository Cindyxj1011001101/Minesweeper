using UnityEngine.UI;
using UnityEngine;
using System;

public class MineNumChange : MonoBehaviour
{
    public void Start()
    {
        EventManager.Instance.AddListener(EventType.ChangeNeighbours, OnChangeNeighbours);
    }
    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener(EventType.ChangeNeighbours, OnChangeNeighbours);
    }
    public void OnChangeNeighbours(int count)
    {
        GetComponent<Text>().text = count.ToString();
    }
}
