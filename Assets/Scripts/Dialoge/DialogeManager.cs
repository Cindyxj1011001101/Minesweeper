using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;  

public class DialogeManager : MonoBehaviour
{
    [Header("对话")]
    [DisplayName("文本文件")]
    public TextAsset textAsset;
    [DisplayName("左图像")]
    public Image Sprite_Left;
    [DisplayName("右图像")]
    public Image Sprite_Right;
    [DisplayName("姓名框")]
    public Text NameText;
    [DisplayName("对话内容")]
    public Text DialogText;
    [DisplayName("角色图像")]
    public List<Sprite> CharacterSprites=new List<Sprite>();
    [DisplayName("角色图像字典")]
    Dictionary<string,Sprite> CharacterSpriteDict=new Dictionary<string,Sprite>();
    private void Awake()
    {
        CharacterSpriteDict["BOSS"]=CharacterSprites[0];
        CharacterSpriteDict["Another_Voice"]=CharacterSprites[1];
    }
    public void UpdateText(string name,string dialog)
    {
        NameText.text = name;
        DialogText.text = dialog;
    }
}
