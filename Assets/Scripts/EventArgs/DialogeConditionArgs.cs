
using UnityEngine;
[System.Serializable]
public class DialogeConditionArgs
{
    public DialogeConditionEnum Condition;
    public Vector2[] BlockPosition;
    public bool[] BlockCondition;
    public string DialogeValue;
    public bool TriggerOnce = true;
}


