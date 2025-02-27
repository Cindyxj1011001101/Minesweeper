using System;

/// <summary>
/// 鼠标按键枚举
/// </summary>
public enum MouseButton
{
    Left,   // 左键
    Right,  // 右键
    Middle  // 中键
}

/// <summary>
/// 事件类型枚举
/// </summary>
public enum EventType
{
    ChangeNeighbours
}

/// <summary>
/// 方块类型枚举
/// </summary>
[Serializable]
public enum BlockType
{
    None = 0,   // 无
    Wall = 1,   // 墙
    Empty=2,  // 空
    Key=3,    // 钥匙
    Mine=4    // 地雷
}

/// <summary>
/// 关卡大类枚举
/// </summary>
public enum LevelCategory
{
    UndergroundLab  // 地下实验室
} 