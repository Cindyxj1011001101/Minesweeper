using UnityEngine;



/// <summary>
/// 玩家移动
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    [DisplayName("移动速度")]
    public float moveSpeed = 5f;// 移动速度
    private Rigidbody2D rb;// 刚体组件
    private Vector2 moveDirection; // 移动方向

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();        // 获取组件
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
        
        Move();//在FixedUpdate中进行物理移动
    }

    private void Move()
    {
        rb.velocity = moveDirection * moveSpeed;// 使用刚体移动
    }


}
