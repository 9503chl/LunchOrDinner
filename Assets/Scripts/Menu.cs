using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public enum Category
{
    None,
    Spicy,
    Crispy,
    NotCraving,
    TooFar,
    Waiting
}

[CreateAssetMenu]
public class Menu : ScriptableObject
{
    public List<string> menu = new List<string>();
    public List<Category> category = new List<Category>() { };
    public List<string> info = new List<string>();
    public List<int> scope = new List<int>();
}

