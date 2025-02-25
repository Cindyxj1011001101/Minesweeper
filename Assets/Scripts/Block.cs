using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;


/// <summary>
/// 方块
/// </summary>
public class Block : MonoBehaviour
{
    public BlockType blockType;//方块类型
    public bool isFlagged;//是否被标记
    public bool isOpened;//是否被打开
    public bool isAround;//是否在玩家周围八格
    public LevelCategory levelCategory;//关卡大类

    private void Start()
    {
        // 添加事件监听
        EventManager.Instance.AddListener<BlockClickEventArgs>(EventType.ClickBlock, OnBlockClicked);
    }

    private void OnDestroy()
    {
        // 移除事件监听
        EventManager.Instance.RemoveListener<BlockClickEventArgs>(EventType.ClickBlock, OnBlockClicked);
    }

    private void OnBlockClicked(BlockClickEventArgs args)
    {
        // 处理点击逻辑
        Debug.Log($"点击了方块: {args.Block}, 按键类型: {args.MouseButton}");

    }
}
