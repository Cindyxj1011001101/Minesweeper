using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(Text))]
public class RoundMineNumChange : MonoBehaviour
{
    public void Start()
    {
        EventManager.Instance.AddListener<int>(EventType.ChangeNeighbours, OnChangeNeighbours);
    }
    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<int>(EventType.ChangeNeighbours, OnChangeNeighbours);
    }
    public void OnChangeNeighbours(int count)
    {
        if(count==int.MaxValue)
        {
            this.GetComponent<Text>().text = "!";
        }
        else
        {
            this.GetComponent<Text>().text = count.ToString();
        }
    }
}
