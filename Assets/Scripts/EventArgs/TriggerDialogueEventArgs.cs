public class TriggerDialogueEventArgs
{
    public DialogeEvent dialogeEvent;
    public Block block;

    public TriggerDialogueEventArgs(DialogeEvent dialogeEvent, Block block)
    {
        this.dialogeEvent = dialogeEvent;
        this.block = block;
    }
}


