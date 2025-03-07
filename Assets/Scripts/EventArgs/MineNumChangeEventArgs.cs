public class MineNumChangeEventArgs
{
    public int countMine { get; set; }
    public int AllMine { get; set; }

    public MineNumChangeEventArgs(int countMine, int allMine)
    {
        this.countMine = countMine;
        this.AllMine = allMine;
    }
} 