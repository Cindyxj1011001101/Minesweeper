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
    [HideInInspector] public Board board;

    public void OnClick(MouseButton mouseButton)
    {
        if(blockType == BlockType.Wall||blockType == BlockType.None||blockType == BlockType.Door_Closed||blockType == BlockType.Door_Opened)
        {
            return;
        }
        // 处理点击逻辑
        if (mouseButton == MouseButton.Left)
        {
            OnLeftClick();
        }
        else if (mouseButton == MouseButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if(isFlagged)
        {
            return;
        }
        if (blockType == BlockType.Empty)
        {
            isOpened = true;//设置为打开
            this.GetComponent<BoxCollider2D>().isTrigger = true;//设置为触发器
            this.GetComponent<SpriteRenderer>().sprite = board.boardSO.blockSprites[(int)BlockType.None];
        }
        else if (blockType == BlockType.Key&&isOpened==false)
        {
            isOpened = true;//设置为打开
            this.GetComponent<BoxCollider2D>().isTrigger = true;//设置为触发器
            EventManager.Instance.TriggerEvent(EventType.OpenKey);//触发钥匙
            this.GetComponent<SpriteRenderer>().sprite = board.boardSO.blockSprites[(int)BlockType.None];
            EventManager.Instance.TriggerEvent(EventType.KeyNumChange, new KeyNumChangeEventArgs(++board.OpenedKeyCount, board.boardSO.keyCount));
        }
        else if (blockType == BlockType.Mine)
        {
            EventManager.Instance.TriggerEvent(EventType.OpenMine);//触发地雷
            this.GetComponent<SpriteRenderer>().sprite = board.boardSO.blockSprites[(int)BlockType.None];
            board.InitBoard();
        }
    }

    public void OnRightClick()
    {
        if (isOpened == false)//未被打开的方块
        {
            isFlagged = !isFlagged;//切换标记状态
            if (isFlagged)
            {
                this.GetComponent<SpriteRenderer>().sprite = board.boardSO.blockSprites[(int)BlockType.Flagged];
                EventManager.Instance.TriggerEvent(EventType.MineNumChange, new MineNumChangeEventArgs(++board.FlaggedMineCount, board.boardSO.mineCount));
                if(this.blockType == BlockType.Mine)
                {
                    board.ActuralMineCount++;
                }
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = board.boardSO.blockSprites[(int)BlockType.Closed];
                EventManager.Instance.TriggerEvent(EventType.MineNumChange, new MineNumChangeEventArgs(--board.FlaggedMineCount, board.boardSO.mineCount));
                if(this.blockType == BlockType.Mine)
                {
                    board.ActuralMineCount--;
                }
            }
            UpdateRadar();
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //如果玩家进入方块触发器
        if (other.gameObject.CompareTag("Player"))
        {
            SetNeighbours();
            UpdateRadar();
            if(this.GetComponent<Open_Door>()!=null&&this.GetComponent<Open_Door>().canLeaveLevel)
            {
                Debug.Log("通关本关");
                EventManager.Instance.TriggerEvent(EventType.TriggerDialogue, new TriggerDialogueEventArgs(DialogeEvent.PassLevel, this));
            }
        }
    }
    public void UpdateRadar()
    {
        board.neighborMineCount = CountNeighboursMine();
        board.neighborKeyCount = CountNeighboursKey();
        if (board.neighborKeyCount)
        {
            EventManager.Instance.TriggerEvent(EventType.ChangeNeighbours, int.MaxValue);
        }
        else
        {
            EventManager.Instance.TriggerEvent(EventType.ChangeNeighbours, board.neighborMineCount);
        }

    }
    public void SetNeighbours()
    {
        board.neighbors = new Block[8];
        if (BoardPos.x - 1 >= 0 && BoardPos.y - 1 >= 0)
        {
            board.neighbors[0] = board.blocks[BoardPos.x - 1, BoardPos.y - 1];
        }
        if (BoardPos.x - 1 >= 0 && BoardPos.y >= 0)
        {
            board.neighbors[1] = board.blocks[BoardPos.x - 1, BoardPos.y];
        }
        if (BoardPos.x - 1 >= 0 && BoardPos.y + 1 < board.boardSO.width)
        {
            board.neighbors[2] = board.blocks[BoardPos.x - 1, BoardPos.y + 1];
        }
        if (BoardPos.x >= 0 && BoardPos.y - 1 >= 0)
        {
            board.neighbors[3] = board.blocks[BoardPos.x, BoardPos.y - 1];
        }
        if (BoardPos.x >= 0 && BoardPos.y + 1 < board.boardSO.width)
        {
            board.neighbors[4] = board.blocks[BoardPos.x, BoardPos.y + 1];
        }
        if (BoardPos.x + 1 < board.boardSO.height && BoardPos.y - 1 >= 0)
        {
            board.neighbors[5] = board.blocks[BoardPos.x + 1, BoardPos.y - 1];
        }
        if (BoardPos.x + 1 < board.boardSO.height && BoardPos.y >= 0)
        {
            board.neighbors[6] = board.blocks[BoardPos.x + 1, BoardPos.y];
        }
        if (BoardPos.x + 1 < board.boardSO.height && BoardPos.y + 1 < board.boardSO.width)
        {
            board.neighbors[7] = board.blocks[BoardPos.x + 1, BoardPos.y + 1];
        }
    }
    public int CountNeighboursMine()
    {
        int count = 0;
        if (board == null)
        {
            Debug.LogError("board 为空");
            return count;
        }
        foreach (var neighbor in board.neighbors)
        {
            if (neighbor == null) continue;  // 跳过空的邻居
            if (neighbor.blockType == BlockType.Mine)//有地雷则计算雷数量
            {
                count++;
            }
            if (neighbor.isFlagged == true)//有标记则减去一个雷
            {
                count--;
            }
        }
        return count;
    }
    public bool CountNeighboursKey()
    {
        foreach (var neighbor in board.neighbors)
        {
            if (neighbor != null && neighbor.blockType == BlockType.Key && neighbor.isOpened == false)
            {
                return true;
            }
        }
        return false;
    }

}
