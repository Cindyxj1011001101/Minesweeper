using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public int width = 9;
    public int height = 9;
    public int mineCount = 10;
    
    public GameObject blockPrefab;
    private Block[,] blocks;

    void Start()
    {
        InitializeBoard();
        PlaceMines();
        CalculateNumbers();
    }

    void InitializeBoard()
    {
        blocks = new Block[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, y, 0);
                GameObject blockObj = Instantiate(blockPrefab, position, Quaternion.identity, transform);
                blocks[x, y] = blockObj.GetComponent<Block>();
            }
        }
    }

    void PlaceMines()
    {
        int minesPlaced = 0;
        while (minesPlaced < mineCount)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            
            if (!blocks[x, y].isMine)
            {
                blocks[x, y].isMine = true;
                minesPlaced++;
            }
        }
    }

    void CalculateNumbers()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!blocks[x, y].isMine)
                {
                    int count = CountAdjacentMines(x, y);
                    blocks[x, y].adjacentMines = count;
                }
            }
        }
    }

    int CountAdjacentMines(int x, int y)
    {
        int count = 0;
        
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newX = x + i;
                int newY = y + j;
                
                if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                {
                    if (blocks[newX, newY].isMine)
                    {
                        count++;
                    }
                }
            }
        }
        
        return count;
    }
} 