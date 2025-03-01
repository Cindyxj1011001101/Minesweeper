using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(Text))]
public class MineNumChange : MonoBehaviour
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
            Debug.Log("周围有钥匙");
            this.GetComponent<Text>().text = "!";
        }
        else
        {
            this.GetComponent<Text>().text = count.ToString();
        }
    }
}
