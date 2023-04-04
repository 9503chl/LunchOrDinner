using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    [SerializeField] Menu m_menu;

    [SerializeField] GameObject Content;

    [SerializeField] Scrollbar scrollbar;

    public List<GameObject> menuContents = new List<GameObject>();
    public List<GameObject> SpicyMenu = new List<GameObject>();
    public List<GameObject> CrispyMenu = new List<GameObject>();
    public List<GameObject> NotCravingMenu = new List<GameObject>();
    public List<GameObject> TooFarMenu = new List<GameObject>();
    public List<GameObject> WaitingMenu = new List<GameObject>();

    Coroutine colorOn;

    void Start()
    {
        for(int i = 0; i<m_menu.menu.Count; i++)
        {
            Add(i);
        }
    }
    public void Add(int index)
    {
        GameObject temp = Instantiate(Content, transform);
        Button button = temp.GetComponentInChildren<Button>();
        Image image = temp.GetComponentInChildren<Image>();
        button.onClick.AddListener(delegate { ImageBrowse(image); });
        Text[] texts = temp.GetComponentsInChildren<Text>();
        texts[0].text = m_menu.menu[index];//이름
        texts[1].text = m_menu.category[index].ToString();//카테고리
        texts[2].text = m_menu.info[index];//설명
        texts[3].text = m_menu.scope[index].ToString();//평점
        menuContents.Add(temp);
        AddMenu(m_menu.category[index], temp);
    }
    public void Remove(int index)
    {
        GameObject target = menuContents[index];
        menuContents.RemoveAt(index);
        RemoveMenu(m_menu.category[index], index);
        Destroy(target);
    }
    public void Search(int index)
    {
        scrollbar.value = (float)index / m_menu.menu.Count;
        GameObject obj = menuContents[index];
        Text[] texts= obj.GetComponentsInChildren<Text>();
        if (colorOn == null)
        {
            colorOn = StartCoroutine(ColorOn(texts));
        }
    }
    public void AddMenu(Category category, GameObject obj)
    {
        switch (category)
        {
            case Category.Spicy :
                SpicyMenu.Add(obj);
                break;
            case Category.Crispy:
                CrispyMenu.Add(obj);
                break;
            case Category.NotCraving:
                NotCravingMenu.Add(obj);
                break;
            case Category.TooFar:
                TooFarMenu.Add(obj);
                break;
            case Category.Waiting:
                WaitingMenu.Add(obj);
                break;
        }
    }
    public void RemoveMenu(Category category, int index)
    {
        switch (category)
        {
            case Category.Spicy:
                SpicyMenu.RemoveAt(index);
                break;
            case Category.Crispy:
                CrispyMenu.RemoveAt(index);
                break;
            case Category.NotCraving:
                NotCravingMenu.RemoveAt(index);
                break;
            case Category.TooFar:
                TooFarMenu.RemoveAt(index);
                break;
            case Category.Waiting:
                WaitingMenu.RemoveAt(index);
                break;
        }
    }
    void ImageBrowse(Image image)//이미지 등록하는거 만들어야함
    {

    }
    IEnumerator ColorOn(Text[] texts)
    {
        for(int i = 0; i< texts.Length; i++)
        {
            texts[i].color = Color.blue;
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = Color.white;
        }
    }
}