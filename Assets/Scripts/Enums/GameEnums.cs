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
    GameStart,
    GameOver,
    ClickBlock,
    BlockOpened,
    BlockFlagged,
    BlockMiddleClicked
}

/// <summary>
/// 方块类型枚举
/// </summary>
public enum BlockType
{
    Wall,   // 墙
    Empty,  // 空
    Key,    // 钥匙
    Mine    // 地雷
}

/// <summary>
/// 关卡大类枚举
/// </summary>
public enum LevelCategory
{
    UndergroundLab  // 地下实验室
} 