using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 地图
/// </summary>
public class Board : MonoBehaviour
{
    [Header("地图")]
    [DisplayName("地图块")]
    public Block[,] blocks;//地图
    [DisplayName("地图数据")]
    public BoardSO boardSO;//地图数据


    [Header("地图块设置")]
    [DisplayName("地图块大小")]
    [Range(0,1)]
    public float blockSize;//方块大小


    public Block[] neighbors=new Block[8];//周围八个方块
    public void Awake()
    {
        boardSO.ValidateData();
        blocks = new Block[boardSO.height, boardSO.width];
        this.GetComponent<GridLayoutGroup>().cellSize = new Vector2(blockSize, blockSize);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(boardSO.width*blockSize, boardSO.height*blockSize);
        InitBoard();
    }
    public void Update()
    {
        
    }
    public void InitBoard()
    {
        for (int i = 0; i < boardSO.height; i++)
        {
            for (int j = 0; j < boardSO.width; j++)
            {
                blocks[i, j] = Instantiate(boardSO.blockPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Block>();
                blocks[i, j].transform.SetParent(transform);
                blocks[i, j].transform.localScale = new Vector3(blockSize, blockSize, 1);
                blocks[i, j].blockType = boardSO.BlocksTypes[i][j];
                blocks[i, j].isFlagged = false;
                blocks[i, j].isOpened = false;
                blocks[i, j].isAround = false;
                blocks[i, j].levelCategory = boardSO.levelCategory;
                blocks[i, j].GetComponent<SpriteRenderer>().sprite = boardSO.blockSprites[(int)boardSO.BlocksTypes[i][j]];
                if(boardSO.BlocksTypes[i][j]==BlockType.None)
                {
                    blocks[i, j].GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
}