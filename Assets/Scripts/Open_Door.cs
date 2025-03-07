using UnityEngine;

public class Open_Door : MonoBehaviour
{
    [HideInInspector]public Board board;
    public void Update(){
        if(board.OpenedKeyCount == board.boardSO.keyCount){
            this.GetComponent<SpriteRenderer>().sprite = board.boardSO.blockSprites[(int)BlockType.Door_Opened];
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            this.GetComponent<Block>().isOpened = true;
            Debug.Log("通关本关");
        }
    }
}
