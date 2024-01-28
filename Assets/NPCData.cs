using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This needs to be located in the Assets folder.
[CreateAssetMenu(fileName = "NPCData", menuName = "ScriptableObjects/NPCScriptableObject", order = 1)]
public class NPCData : ScriptableObject
{
    public int id;
    public string npcName;
    public Sprite img;
    public EmojiEnum[] acceptableEmoji;
}
