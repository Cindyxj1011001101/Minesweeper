using UnityEngine;
using UnityEditor;
using System.ComponentModel;

public class Click : MonoBehaviour
{
    [Header("射线设置")]
    [DisplayName("最大射线距离")]
    public float maxDistance = 100f;

    public void Update()
    {
        // 左键点击
        if (Input.GetMouseButtonDown(0))
        {
            BlockDetect(MouseButton.Left);
        }
        // 右键点击
        else if (Input.GetMouseButtonDown(1))
        {
            BlockDetect(MouseButton.Right);
        }
        // 中键点击
        else if (Input.GetMouseButtonDown(2))
        {
            BlockDetect(MouseButton.Middle);
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
            Block block = clickedObject.GetComponent<Block>();
            if (block != null)
            {
                var args = new BlockClickEventArgs(block, mouseButton);
                EventManager.Instance.TriggerEvent(EventType.ClickBlock, args);
            }
        }
    }
}
