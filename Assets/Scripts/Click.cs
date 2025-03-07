using UnityEngine;
using UnityEditor;
using System.ComponentModel;

public class Click : MonoBehaviour
{
    [Header("射线设置")]
    [DisplayName("最大射线距离")]
    public float maxDistance = 100f;

    [Header("点击设置")]
    [DisplayName("点击冷却时间")]
    public float clickCooldown = 0.5f;    // 点击冷却时间
    private float nextClickTime = 0f;      // 下一次可以点击的时间

    public Board board;

    public void Update()
    {
        if (!GlobalData.Instance.isDialogueMode)
        {
            // 检查是否可以点击
            if (Time.time < nextClickTime) return;

            // 左键点击
            if (Input.GetMouseButtonDown(0))
            {
                BlockDetect(MouseButton.Left);
                nextClickTime = Time.time + clickCooldown;  // 设置下一次可点击时间
            }
            // 右键点击
            else if (Input.GetMouseButtonDown(1))
            {
                BlockDetect(MouseButton.Right);
                nextClickTime = Time.time + clickCooldown;
            }
            // 空格键点击
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                SpaceDetect();
                nextClickTime = Time.time + clickCooldown;
            }
        }

    }
    /// <summary>
    /// 方块点击检测
    /// </summary>
    public void BlockDetect(MouseButton mouseButton)
    {
        // 获取鼠标在屏幕上的位置
        Vector2 mousePosition = Input.mousePosition;
        // 将屏幕坐标转换为世界坐标的射线
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        // 射线检测
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, maxDistance);
        if (hit.collider != null)
        {
            // 获取点击到的物体
            GameObject clickedObject = hit.collider.gameObject;
            // 获取Block组件（如果有的话）
            Block clickedBlock = clickedObject.GetComponent<Block>();
            if (clickedBlock != null)
            {
                for (int i = 0; i < board.neighbors.Length; i++)
                {

                    if (board.neighbors[i] == clickedBlock)
                    {
                        clickedBlock.OnClick(mouseButton);
                        return;
                    }
                }

            }
        }
    }

    /// <summary>
    /// 空格键点击检测
    /// </summary>
    public void SpaceDetect()
    {
        if (board.neighborMineCount == 0)
        {
            foreach (Block neighbor in board.neighbors)
            {
                if (neighbor != null && neighbor.isFlagged == false)
                {
                    neighbor.OnClick(MouseButton.Left);
                }
            }
        }
    }
}
