using UnityEngine;
using System;
using UnityEditor;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "NewBoard", menuName = "MineSweeper/Board Data")]
public class BoardSO : ScriptableObject
{
    [Header("基础设置")]
    [DisplayName("地图宽度")]
    public int width;              // 地图宽度
    [DisplayName("地图高度")]
    public int height;             // 地图高度
    [DisplayName("地雷总数")]
    public int mineCount;         // 地雷数量
    [DisplayName("钥匙总数")]
    public int keyCount;           // 钥匙数量
    [DisplayName("所属地图类型")]
    public LevelCategory levelCategory;

    [Header("地图数据")]
    [DisplayName("地图数据CSV文件")]
    public TextAsset BoardBlockCSV;
    [DisplayName("地图数据")]
    public List<List<BlockType>> BlocksTypes=new List<List<BlockType>>();


    [Header("预制体设置")]
    [DisplayName("方块预制体")]
    public GameObject blockPrefab;

    [Header("方块图片")]  
    public Sprite[] blockSprites;

    // 验证数据是否有效
    public bool ValidateData()
    {
        if (width <= 0 || height <= 0)
        {
            Debug.LogError("地图尺寸必须大于0");
            return false;
        }

        if (mineCount <= 0 || mineCount >= width * height)
        {
            Debug.LogError("地雷数量无效");
            return false;
        }

        if (blockPrefab == null)
        {
            Debug.LogError("未设置方块预制体");
            return false;
        }

        if (BoardBlockCSV == null)
        {
            Debug.LogError("未设置地图数据CSV文件");
            return false;
        }
        else
        {
            BlocksTypes = CSVReader.ReadBlockTypeCSV(BoardBlockCSV);
            //Debug.Log("地图数据CSV文件读取成功");
        }

        return true;
    }
}
