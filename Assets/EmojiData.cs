using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This needs to be located in the Assets folder.
[CreateAssetMenu(fileName = "EmojiData", menuName = "ScriptableObjects/EmojiScriptableObject", order = 1)]
public class EmojiData : ScriptableObject
{
    public EmojiEnum id;
    public Sprite img;
}
