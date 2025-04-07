using UnityEngine;

public class Open_Door : MonoBehaviour
{
    public bool canLeaveLevel = false;
    public string sceneName;
    [HideInInspector]public Board board;
    public void Update(){
        if(board!=null && board.OpenedKeyCount == board.boardSO.keyCount){
            this.GetComponent<SpriteRenderer>().sprite = board.boardSO.blockSprites[(int)BlockType.Door_Opened];
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            this.GetComponent<Block>().isOpened = true;
            canLeaveLevel = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canLeaveLevel&&!GlobalData.Instance.isDialogueMode)
        {
            FindObjectOfType<SceneFader>().FadeToScene(sceneName);
        }
    }
}
