using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu (menuName = "Character")]
public class Character : ScriptableObject {

    public string characterName = "Default";
    public string faction = "Default";
    public int HP = 100;

    public List<Sprite> charSprite;
    public AnimatorController ac;
}
