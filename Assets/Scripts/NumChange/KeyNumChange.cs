using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(Text))]
public class KeyNumChange : MonoBehaviour
{
    public void Start()
    {
        EventManager.Instance.AddListener<KeyNumChangeEventArgs>(EventType.KeyNumChange, OnChangeKey);
    }
    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<KeyNumChangeEventArgs>(EventType.KeyNumChange, OnChangeKey);
    }
    public void OnChangeKey(KeyNumChangeEventArgs args)
    {
        this.GetComponent<Text>().text = args.countKey.ToString()+"/"+args.AllKey.ToString();
    }
}
