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
    [Header("方块属性")]
    [DisplayName("方块类型")]
    public BlockType blockType;//方块类型
    [DisplayName("是否被标记")]
    public bool isFlagged;//是否被标记
    [DisplayName("是否被打开")]
    public bool isOpened;//是否被打开
    [DisplayName("是否在玩家周围八格")]
    public bool isAround;//是否在玩家周围八格
    [DisplayName("关卡大类")]
    public LevelCategory levelCategory;//关卡大类
    [DisplayName("方块在地图中的位置")]
    public Vector2Int BoardPos;//方块在地图中的位置
    private Board board;

    public void OnClick(MouseButton mouseButton)
    {
        // 处理点击逻辑
        Debug.Log($"点击了方块: {this}, 按键类型: {mouseButton}");

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //如果玩家进入方块触发器
        if (other.gameObject.CompareTag("Player"))
        {
            SetNeighbours(other.GetComponentInParent<Board>());
            int count = CountNeighbours();
            EventManager.Instance.TriggerEvent(EventType.ChangeNeighbours, count);
        }
    }
    public void SetNeighbours(Board board)
    {
        board.neighbors[0] = board.blocks[BoardPos.x - 1, BoardPos.y - 1];
        board.neighbors[1] = board.blocks[BoardPos.x - 1, BoardPos.y];
        board.neighbors[2] = board.blocks[BoardPos.x - 1, BoardPos.y + 1];
        board.neighbors[3] = board.blocks[BoardPos.x, BoardPos.y - 1];
        board.neighbors[4] = board.blocks[BoardPos.x, BoardPos.y + 1];
        board.neighbors[5] = board.blocks[BoardPos.x + 1, BoardPos.y - 1];
        board.neighbors[6] = board.blocks[BoardPos.x + 1, BoardPos.y];
        board.neighbors[7] = board.blocks[BoardPos.x + 1, BoardPos.y + 1];

    }
    public int CountNeighbours()
    {
        int count = 0;
        foreach (var neighbor in board.neighbors)
        {
            if (neighbor.blockType == BlockType.Key)
                return -1;
            if (neighbor != null && neighbor.blockType == BlockType.Mine)
            {
                count++;
            }
        }
        return count;
    }

}
