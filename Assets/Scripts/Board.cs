using UnityEngine;

/// <summary>
/// 地图
/// </summary>
public class Board : MonoBehaviour
{
    public Block[,] blocks;//地图
    public int KeyNum;//钥匙数量
    public int MineNum;//地雷数量
    public Block[] neighbors;//周围八个方块
}