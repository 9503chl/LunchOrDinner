using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    [SerializeField] Menu m_menu;

    [SerializeField] Text TextInstance;

    int index;

    void Start()
    {
        index = m_menu.menu.Count;
        Transform TargetTransform = transform.GetChild(0).GetChild(0);//Content
        for(int i = 0; i<m_menu.menu.Count; i++)
        {
            Instantiate(TextInstance, new Vector3(), Quaternion.Euler(0,0,0),TargetTransform);
        }
    }
    public void Add()
    {

    }
    public void Remove()//지우는게 좀 어려움, 다시 하나씩 다 당겨야함.
    {

    }
    
}