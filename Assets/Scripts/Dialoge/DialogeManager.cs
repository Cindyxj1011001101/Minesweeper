using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;

public class DialogeManager : MonoBehaviour
{
    [Header("对话")]
    [DisplayName("对话")]
    public GameObject Dialoge;
    [DisplayName("对话文本")]
    public TextAsset DialogContent;
    public DialogeConditionArgs[] DialogCondition;

    [DisplayName("左图像")]
    public Image Sprite_Left;
    [DisplayName("姓名框")]
    public Text NameText;
    [DisplayName("对话内容")]
    public Text DialogText;
    public Dictionary<string, Sprite> CharacterSpriteDict = new Dictionary<string, Sprite>();
    public CharacterSpriteEventArgs[] CharacterSprite;
    public List<List<string>> dialogeRows = new List<List<string>>();
    public int dialogIndex;

    public Board board;

    public void Start()
    {
        EventManager.Instance.AddListener<TriggerDialogueEventArgs>(EventType.TriggerDialogue, TriggerDialogue);
    }
    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener<TriggerDialogueEventArgs>(EventType.TriggerDialogue, TriggerDialogue);
    }

    public void Update()
    {
        if (GlobalData.Instance.isDialogueMode&&Input.GetMouseButtonDown(0))
        {
            UpdateDialog();
        }
    }

    public void TriggerDialogue(TriggerDialogueEventArgs args)
    {
        if (args.dialogeEvent == DialogeEvent.EnterLevel)
        {
            //进入区域触发
            EnterLevelDialogue();
        }
        else if (args.dialogeEvent == DialogeEvent.PassLevel)
        {
            //当前钥匙数=总钥匙数&&所有雷都被标出
            if (board.OpenedKeyCount == board.boardSO.keyCount && board.ActuralMineCount == board.boardSO.mineCount)
            {
                PerfectPassDialogue();
            }
            else
            {
                NormalPassDialogue();
            }
        }
        else if (args.dialogeEvent == DialogeEvent.LeftClickBlock)
        {
            LeftClickBlockDialogue(args.block);
        }
        else if (args.dialogeEvent == DialogeEvent.RightClickBlock)
        {
            RightClickBlockDialogue(args.block);
        }
        else if (args.dialogeEvent == DialogeEvent.EnterBlock)
        {
            EnterBlockDialogue(args.block);
        }
        else if (args.dialogeEvent == DialogeEvent.Die)
        {
            DieDialogue();
        }
    }
    #region 触发剧情判断
    public void EnterLevelDialogue()
    {
        foreach (var dialog in DialogCondition)
        {
            if (dialog.Condition == DialogeConditionEnum.EnterLevel && dialog.TriggerOnce)
            {
                dialog.TriggerOnce = false;
                ShowFirstDialogue(dialog.DialogeValue);
            }
        }
    }
    public void PerfectPassDialogue()
    {
        foreach (var dialog in DialogCondition)
        {
            if (dialog.Condition == DialogeConditionEnum.PerfactPass && dialog.TriggerOnce)
            {
                dialog.TriggerOnce = false;
                ShowFirstDialogue(dialog.DialogeValue);
            }
        }
    }
    public void NormalPassDialogue()
    {
        foreach (var dialog in DialogCondition)
        {
            if (dialog.Condition == DialogeConditionEnum.NormalPass && dialog.TriggerOnce)
            {
                dialog.TriggerOnce = false;
                ShowFirstDialogue(dialog.DialogeValue);
            }
        }
    }
    public void LeftClickBlockDialogue(Block block)
    {
        foreach (var dialog in DialogCondition)
        {
            //点击一个方块，这个方块在对话条件中
            if (dialog.Condition == DialogeConditionEnum.ClickOneBlock && dialog.BlockPosition.Contains(block.BoardPos) && dialog.TriggerOnce)
            {
                dialog.TriggerOnce = false;
                ShowFirstDialogue(dialog.DialogeValue);
            }
        }
    }
    public void RightClickBlockDialogue(Block block)
    {
        foreach (var dialog in DialogCondition)
        {
            if (dialog.Condition == DialogeConditionEnum.FlagOneBlock && dialog.BlockPosition.Contains(block.BoardPos) && dialog.TriggerOnce)
            {
                dialog.TriggerOnce = false;
                ShowFirstDialogue(dialog.DialogeValue);
            }
        }
    }
    public void EnterBlockDialogue(Block block)
    {
        foreach (var dialog in DialogCondition)
        {
            if (dialog.Condition == DialogeConditionEnum.EnterBlock && dialog.BlockPosition.Contains(block.BoardPos) && dialog.TriggerOnce)
            {
                dialog.TriggerOnce = false;
                ShowFirstDialogue(dialog.DialogeValue);
            }
        }
    }
    public void DieDialogue()
    {
        foreach (var dialog in DialogCondition)
        {
            if (dialog.Condition == DialogeConditionEnum.Die && dialog.TriggerOnce)
            {
                dialog.TriggerOnce = false;
                ShowFirstDialogue(dialog.DialogeValue);
            }
        }
    }
    #endregion

    #region 初始化
    public void Awake()
    {
        ReadContent(DialogContent);
        TransDict();
    }
    #endregion

    #region 读取对话文件
    public void ReadContent(TextAsset _textAsset)
    {
        string[] rows = _textAsset.text.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            List<string> cells = rows[i].Split(',').ToList();
            dialogeRows.Add(cells);
        }
    }
    #endregion

    #region 转换字典
    public void TransDict()
    {
        foreach (var item in CharacterSprite)
        {
            CharacterSpriteDict.Add(item.name.ToString(), item.sprite);
        }
    }
    #endregion

    #region 更新对话内容
    public void UpdateText(string name, string dialog, string position)
    {
        NameText.text = name;
        DialogText.text = dialog;
        if (position == "Left")
        {
            Sprite_Left.sprite = CharacterSpriteDict[name];
        }
    }
    #endregion

    #region 切换到本句对话
    public void UpdateDialog()
    {
        foreach (var row in dialogeRows)
        {
            if (row[0] == "#"&&int.Parse(row[2]) == dialogIndex)
            {
                Debug.Log(row[0]+row[1]+row[2]+row[3]+row[4]+row[5]+row[6]);
                UpdateText(row[3], row[5], row[4]);
                dialogIndex = int.Parse(row[6]);
                break;
            }
            if (row[0] == "End"&&int.Parse(row[2]) == dialogIndex)
            {
                Debug.Log("End："+row[0]+row[1]+row[2]+row[3]+row[4]+row[5]+row[6]);
                Dialoge.SetActive(false);
                GlobalData.Instance.isDialogueMode = false;
            }
        }
    }
    #endregion

    #region 显示第一句对话
    public void ShowFirstDialogue(string dialogueValue)
    {
        Dialoge.SetActive(true);
        GlobalData.Instance.isDialogueMode = true;

        foreach (var row in dialogeRows)
        {
            if (row[0] == "#" && row[1] == dialogueValue)
            {
                dialogIndex = int.Parse(row[6]);
                UpdateText(row[3], row[5], row[4]);
                break;
            }
        }
    }
    #endregion

}
