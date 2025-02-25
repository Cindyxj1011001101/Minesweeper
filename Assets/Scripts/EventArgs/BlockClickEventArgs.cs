public class BlockClickEventArgs
{
    public Block Block { get; set; }
    public MouseButton MouseButton { get; set; }

    public BlockClickEventArgs(Block block, MouseButton mouseButton)
    {
        Block = block;
        MouseButton = mouseButton;
    }
} 