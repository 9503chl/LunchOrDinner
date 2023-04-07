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
public struct Menus {
    public Sprite images;
    public string menu;
    public Category category;
    public string info;
    public int scope;
}
[CreateAssetMenu]
public class Menu : ScriptableObject
{
    public List<Menus> MenuList = new List<Menus>();
}