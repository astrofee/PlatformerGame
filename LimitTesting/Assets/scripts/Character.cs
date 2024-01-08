using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject 
{
    public GameObject model;
    public int attack;
    public int defense;
    public int speed;
    public string name;
    public int maxHealth;
    public int currentHealth;
}
