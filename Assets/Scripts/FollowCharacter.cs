using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    [DisplayName("摄像机移动速度")]
    public float CameramoveSpeed = 10f;
    public void Start()
    {
        EventManager.Instance.AddListener<Vector2>(EventType.CameraMove, OnPlayerMove);
    }
    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<Vector2>(EventType.CameraMove, OnPlayerMove);
    }
    public void OnPlayerMove(Vector2 moveDirection)
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(moveDirection.x, moveDirection.y), Time.deltaTime * CameramoveSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
