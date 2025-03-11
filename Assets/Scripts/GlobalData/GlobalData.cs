using UnityEngine;

/// <summary>
/// 全局数据
/// </summary>
public class GlobalData:Singleton<GlobalData>
{
    [Header("地图")]
    [DisplayName("地图")]
    public Board board;
    [DisplayName("是否为剧情模式")]
    public bool isDialogueMode = false;

    public void Awake()
    {
    }

}
