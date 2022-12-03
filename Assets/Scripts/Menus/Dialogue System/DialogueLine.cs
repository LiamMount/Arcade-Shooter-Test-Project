using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public Sprite speakerSprite;
    //Sound?

    [TextArea]
    public string dialogue;
}
