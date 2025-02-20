using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isMine = false;        // 是否是地雷
    public bool isRevealed = false;    // 是否已经被揭示
    public bool isFlagged = false;     // 是否被标记为地雷
    public int adjacentMines = 0;      // 周围地雷数量
    
    private SpriteRenderer spriteRenderer;
    
    [Header("Sprites")]
    public Sprite[] numberSprites;     // 数字精灵（0-8）
    public Sprite mineSprite;          // 地雷精灵
    public Sprite hiddenSprite;        // 未揭示状态的精灵
    public Sprite flagSprite;          // 旗帜精灵


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = hiddenSprite;
    }

    public void OnMouseClick()
    {
        if (!isFlagged && !isRevealed)
        {
            Reveal();
        }
    }

    public void OnMouseRightClick()
    {
        if (!isRevealed)
        {
            ToggleFlag();
        }
    }

    public void Reveal()
    {
        isRevealed = true;
        
        if (isMine)
        {
            spriteRenderer.sprite = mineSprite;
            // TODO: 游戏结束逻辑
        }
        else
        {
            spriteRenderer.sprite = numberSprites[adjacentMines];
        }
    }

    private void ToggleFlag()
    {
        isFlagged = !isFlagged;
        spriteRenderer.sprite = isFlagged ? flagSprite : hiddenSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
