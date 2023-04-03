using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    [SerializeField] Menu m_menu;

    [SerializeField] GameObject Content;

    [SerializeField] Scrollbar scrollbar; 

    List<GameObject> menuContents = new List<GameObject>();

    Coroutine colorOn;

    void Start()
    {
        for(int i = 0; i<m_menu.menu.Count; i++)
        {
            GameObject temp = Instantiate(Content,transform);
            Text[] texts = temp.GetComponentsInChildren<Text>();
            texts[0].text = m_menu.menu[i];//이름
            texts[1].text = m_menu.category[i].ToString();//카테고리
            texts[2].text = m_menu.info[i];//설명
            texts[3].text = m_menu.scope[i].ToString();//평점
            menuContents.Add(temp);
        }
    }
    public void Add()
    {
        GameObject temp = Instantiate(Content, transform);
        menuContents.Add(temp);
        Text[] texts = temp.GetComponentsInChildren<Text>();
        int index = m_menu.menu.Count-1;
        texts[0].text = m_menu.menu[index];//이름
        texts[1].text = m_menu.category[index].ToString();//카테고리
        texts[2].text = m_menu.info[index];//설명
        texts[3].text = m_menu.scope[index].ToString();//평점
    }
    public void Remove(int index)
    {
        GameObject target = menuContents[index];
        menuContents.RemoveAt(index);
        Destroy(target);
    }
    public void Search(int index)
    {
        scrollbar.value = (float)index / m_menu.menu.Count;
        GameObject obj = menuContents[index];
        Text[] texts= obj.GetComponentsInChildren<Text>();
        colorOn = StartCoroutine(ColorOn(texts));
    }

    IEnumerator ColorOn(Text[] texts)
    {
        texts[0].color = Color.blue;
        texts[1].color = Color.blue;
        texts[2].color = Color.blue;
        texts[3].color = Color.blue;
        yield return new WaitForSeconds(5f);
        texts[0].color = Color.white;
        texts[1].color = Color.white;
        texts[2].color = Color.white;
        texts[3].color = Color.white;
    }
}