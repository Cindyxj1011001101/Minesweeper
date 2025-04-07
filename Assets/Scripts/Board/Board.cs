using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏地图类，负责管理和初始化游戏地图
/// </summary>
public class Board : MonoBehaviour
{
    [Header("地图")]
    [DisplayName("地图块")]
    public Block[,] blocks;  // 二维数组存储地图上所有方块
    [DisplayName("地图数据")]
    public BoardSO boardSO;  // 存储地图配置数据的ScriptableObject

    [Header("地图块设置")]
    [DisplayName("地图块大小")]
    [Range(0, 1)]
    public float blockSize;  // 控制地图块的显示大小

    [Header("玩家对象")] public GameObject player;

    /// <summary>
    /// 存储玩家周围8个相邻的方块
    /// 顺序：左上、上、右上、左、右、左下、下、右下
    /// </summary>
    [HideInInspector] public Block[] neighbors = new Block[8];
    [HideInInspector] public int neighborMineCount = 0;
    [HideInInspector] public bool neighborKeyCount = false;
    [HideInInspector] public int FlaggedMineCount = 0;
    [HideInInspector] public int OpenedKeyCount = 0;
    [HideInInspector] public int ActuralMineCount = 0;

    /// <summary>
    /// 初始化时调用，设置地图基本参数并创建地图
    /// </summary>
    public void Start()
    {
        boardSO.ValidateData();  // 验证地图数据的有效性
        blocks = new Block[boardSO.height, boardSO.width];  // 根据配置初始化地图数组
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(blockSize, blockSize);  // 设置网格布局的单元格大小
        GetComponent<RectTransform>().sizeDelta = new Vector2(boardSO.width * blockSize, boardSO.height * blockSize);  // 设置地图整体大小
        InitBoard();
        EventManager.Instance.TriggerEvent(EventType.TriggerDialogue, new TriggerDialogueEventArgs(DialogeEvent.EnterLevel, null));
        //触发初次进入关卡的剧情
    }
    /// <summary>
    /// 初始化地图，创建并设置所有地图块
    /// </summary>
    public void InitBoard()
    {

        // 获取所有子物体
        for (int i = 0; i < this.transform.childCount; i++)
        {
            blocks[i / boardSO.width, i % boardSO.width] = this.transform.GetChild(i).GetComponent<Block>();
        }
        for (int i = 0; i < boardSO.height; i++)
        {
            for (int j = 0; j < boardSO.width; j++)
            {
                // 实例化地图块预制体
                blocks[i, j].transform.localScale = new Vector3(blockSize, blockSize, 1);  // 设置大小

                // 设置地图块属性
                blocks[i, j].blockType = boardSO.BlocksTypes[i][j];  // 设置方块类型
                blocks[i, j].isFlagged = false;  // 初始化标记状态
                blocks[i, j].isOpened = false;   // 初始化打开状态
                blocks[i, j].isAround = false;   // 初始化周围状态
                blocks[i, j].levelCategory = boardSO.levelCategory;  // 设置关卡类别
                blocks[i, j].BoardPos = new Vector2Int(i, j);  // 设置方块在地图中的位置
                blocks[i, j].board = this.GetComponent<Board>();
                // 设置地图块外观
                if (boardSO.BlocksTypes[i][j] == BlockType.None || boardSO.BlocksTypes[i][j] == BlockType.Wall || boardSO.BlocksTypes[i][j] == BlockType.Door_Closed)
                {
                    blocks[i, j].GetComponent<SpriteRenderer>().sprite = boardSO.blockSprites[(int)boardSO.BlocksTypes[i][j]];
                }
                else
                {
                    blocks[i, j].GetComponent<SpriteRenderer>().sprite = boardSO.blockSprites[(int)BlockType.Closed];
                }
                //如果该方块为门方块则在对象上增加Open_Door组件
                if (boardSO.BlocksTypes[i][j] == BlockType.Door_Closed)
                {
                    blocks[i, j].gameObject.AddComponent<Open_Door>();
                    blocks[i, j].GetComponent<Open_Door>().board = this;
                    blocks[i, j].GetComponent<Open_Door>().sceneName = boardSO.nextSceneName;
                }

                // 如果是空方块，启用触发器
                if (boardSO.BlocksTypes[i][j] == BlockType.None)
                {
                    blocks[i, j].GetComponent<BoxCollider2D>().isTrigger = true;
                }
                //设定方块视觉层级
                blocks[i, j].GetComponent<SpriteRenderer>().sortingOrder = i;
            }
        }
        EventManager.Instance.TriggerEvent(EventType.KeyNumChange, new KeyNumChangeEventArgs(0, boardSO.keyCount));
        EventManager.Instance.TriggerEvent(EventType.MineNumChange, new MineNumChangeEventArgs(0, boardSO.mineCount));
        EventManager.Instance.TriggerEvent(EventType.PlayerPosChange, boardSO.playerStartPos);
    }
}