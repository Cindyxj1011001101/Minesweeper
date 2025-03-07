public class KeyNumChangeEventArgs
{
    public int countKey { get; set; }
    public int AllKey { get; set; }

    public KeyNumChangeEventArgs(int countKey, int allKey)
    {
        this.countKey = countKey;
        this.AllKey = allKey;
    }
} 