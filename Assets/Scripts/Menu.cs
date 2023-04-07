using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public enum Category
{
    None,//0
    Spicy,//1
    Crispy,//2
    NotCraving,//3
    TooFar,//4
    Waiting//5
}

[CreateAssetMenu]
public class Menu : ScriptableObject
{
    public List<Sprite> images = new List<Sprite>();
    public List<string> menu = new List<string>();
    public List<Category> category = new List<Category>() { };
    public List<string> info = new List<string>();
    public List<int> scope = new List<int>();
}