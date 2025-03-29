using UnityEngine;

/// <summary>
/// 全局数据
/// </summary>
public class GlobalData:Singleton<GlobalData>
{
    public Board board;
    [HideInInspector]public bool isDialogueMode = false;

}
