using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

/// <summary>
/// 玩家移动
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    [DisplayName("移动速度")]
    public float moveSpeed = 10f;
    [DisplayName("当前位置")]
    public Vector2 PresentBlock;

    [Header("点击设置")]
    [DisplayName("移动冷却时间")]
    public float moveCooldown = 0.1f;    // 移动冷却时间
    private float nextMoveTime = 0f;      // 下一次可以移动的时间
    [SerializeField]
    public Board board;
    public bool EnterLevel = true;
    public Sprite InitSprite;

    private Rigidbody2D rb;
    private Animator ani;
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
        EventManager.Instance.AddListener<Vector2Int>(EventType.PlayerPosChange, OnPlayerPosChange);
    }
    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<Vector2Int>(EventType.KeyNumChange, OnPlayerPosChange);
    }
    public void OnPlayerPosChange(Vector2Int args)
    {
        PresentBlock = args;
        if (ani != null)
        {
                    ani.SetFloat("Horizontal",-1);
        }
        Vector2 targetPosition = GetTargetPosition(Vector2.zero);
        transform.position = targetPosition;
    }
    private void Update()
    {
        if (EnterLevel)
        {
            this.transform.position = board.blocks[(int)PresentBlock.x, (int)PresentBlock.y].transform.position;
            EnterLevel = false;
        }
        if (!GlobalData.Instance.isDialogueMode)
        {
            // 检查是否可以移动
            if (!GlobalData.Instance.isDialogueMode)
            {
                if (Time.time < nextMoveTime) return;
                //获取按键输入
                Vector2 moveDirection = GetInput();
                Move(moveDirection);
            }
        }


    }
    public void Move(Vector2 moveDirection)
    {

        //获取目标位置
        Vector2 targetPosition = GetTargetPosition(moveDirection);
        if (targetPosition != new Vector2(transform.position.x, transform.position.y))
        {
            if (moveDirection.x != 0 || moveDirection.y != 0)
            {
                ani.SetFloat("Horizontal", moveDirection.x);
                ani.SetFloat("Vertical", moveDirection.y);
                ani.SetBool("IsWalk", true);
            }
            else
            {
                ani.SetBool("IsWalk", false);
            }
        }
        //触发摄像机移动
        EventManager.Instance.TriggerEvent(EventType.CameraMove, targetPosition);
        //触发对话进入方块检测
        EventManager.Instance.TriggerEvent(EventType.TriggerDialogue,new TriggerDialogueEventArgs(DialogeEvent.EnterLevel, GetComponent<Block>()));
        //移动
        this.transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
    //获取目标移动位置
    private Vector2 GetTargetPosition(Vector2 moveDirection)
    {
        //获取目标块
        Vector2 TargetBlock = PresentBlock + moveDirection;
        //目标块判断
        //如果不在棋盘内则不移动
        if (TargetBlock.x < 0 || TargetBlock.x >= board.boardSO.height || TargetBlock.y < 0 || TargetBlock.y >= board.boardSO.width)
        {
            Debug.Log("不在棋盘内");
            return board.blocks[(int)PresentBlock.x, (int)PresentBlock.y].transform.position;
        }
        //如果目标块是空块或打开则移动
        if (board.blocks[(int)TargetBlock.x, (int)TargetBlock.y].blockType == BlockType.None || board.blocks[(int)TargetBlock.x, (int)TargetBlock.y].isOpened == true)
        {
            //更新当前所在方块
            PresentBlock = TargetBlock;
            Vector2 TargetPos = board.blocks[(int)TargetBlock.x, (int)TargetBlock.y].transform.position;
            //获取目标块位置
            return TargetPos;
        }
        Debug.Log("目标块不可前往");
        return board.blocks[(int)PresentBlock.x, (int)PresentBlock.y].transform.position;


    }
    public Vector2 GetInput()
    {
        //获取输入
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextMoveTime = Time.time + moveCooldown;  // 设置下一次可移动时间
            return new Vector2(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            nextMoveTime = Time.time + moveCooldown;
            return new Vector2(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            nextMoveTime = Time.time + moveCooldown;
            return new Vector2(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextMoveTime = Time.time + moveCooldown;
            return new Vector2(0, 1);
        }
        return new Vector2(0, 0);
    }
}
