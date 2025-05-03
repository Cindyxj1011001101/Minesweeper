using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 全局数据
/// </summary>
public class GlobalData:MonoBehaviour
{
    public  static GlobalData Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public Board board;
    [HideInInspector]public bool isDialogueMode = false;
    public Image Message;

}
