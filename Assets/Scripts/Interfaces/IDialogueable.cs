using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueable
{
    public Sprite DialogueSprite { get; protected set; }
    public TextAsset InkStory { get; protected set; }
}
