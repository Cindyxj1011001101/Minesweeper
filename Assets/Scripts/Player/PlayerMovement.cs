using UnityEngine;



/// <summary>
/// 玩家移动
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    [SerializeField] private float moveSpeed = 5f;        // 移动速度
    private Rigidbody2D rb;                              // 刚体组件
    private Vector2 moveDirection;                        // 移动方向

    private void Start()
    {
        // 获取组件
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 获取输入
        float moveX = Input.GetAxisRaw("Horizontal");    // A/D
        float moveY = Input.GetAxisRaw("Vertical");      // W/S

        // 存储移动方向
        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    private void FixedUpdate()
    {
        // 在FixedUpdate中进行物理移动
        Move();
    }

    private void Move()
    {
        // 使用刚体移动
        rb.velocity = moveDirection * moveSpeed;
    }


}
