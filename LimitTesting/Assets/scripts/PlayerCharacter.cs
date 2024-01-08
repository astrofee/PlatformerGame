using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Player Character", menuName = "Character/Player Character")]
public class PlayerCharacter : Character
{
    public string fullname;
    public Sprite icon;
    public GameObject overworldModel;
}
