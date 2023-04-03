using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] Menu m_menu;

    [SerializeField] UnityEngine.UI.Button MenuOpen;

    [SerializeField] UnityEngine.UI.Button SelectButton;

    [SerializeField] UnityEngine.UI.Button CommitButton;

    [SerializeField] UnityEngine.UI.Button RevokeButton;

    [SerializeField] UnityEngine.UI.Button SearchButton;

    [SerializeField] UnityEngine.UI.Button BackButton;

    [SerializeField] Text[] CommitInput;

    [SerializeField] Text RevokeInput;

    [SerializeField] Text[] ResultText;

    [SerializeField] UnityEngine.UI.Image NoticePanel;

    [SerializeField] GameObject menuScroll;

    MenuScroll ms;

    Coroutine TextCoroutine;
    void Start()
    {
        CommitButton.onClick.AddListener(CommitMenu);
        SelectButton.onClick.AddListener(SelectMenu);
        RevokeButton.onClick.AddListener(RevokeMenu);
        SearchButton.onClick.AddListener(SearchMenu);
        BackButton.onClick.AddListener(Back);
        ms = menuScroll.GetComponent<MenuScroll>();
    }

    void CommitMenu()
    {
        if (!m_menu.menu.Contains(CommitInput[0].text))
        {
            m_menu.menu.Add(CommitInput[0].text);
            m_menu.info.Add(CommitInput[1].text);
            m_menu.scope.Add(int.Parse(CommitInput[2].text));
            ms.Add();
        }
        else if(TextCoroutine == null)
        {
            TextCoroutine = StartCoroutine(Notice("메뉴가 이미 존재합니다."));
        }
    }
    void SelectMenu()//선택
    {
        int index = m_menu.menu.Count;
        ResultText[0].text = m_menu.menu[Random.Range(0, index)];
        ResultText[1].text = m_menu.info[Random.Range(0, index)];
        ResultText[2].text = m_menu.scope[Random.Range(0, index)].ToString();
    }
    void RevokeMenu()
    {
        if (m_menu.menu.Contains(RevokeInput.text))
        {
            int index = m_menu.menu.IndexOf(RevokeInput.text);
            m_menu.menu.Remove(RevokeInput.text);
            m_menu.info.RemoveAt(index);
            m_menu.scope.RemoveAt(index);
            ms.Remove(index);
        }
        else if (TextCoroutine == null)
        {
            TextCoroutine = StartCoroutine(Notice("메뉴가 존재하지 않습니다."));
        }
    }
    void SearchMenu()
    {
        if (m_menu.menu.Contains(RevokeInput.text))
        {
            int index = m_menu.menu.IndexOf(RevokeInput.text);
            ms.Search(index);
        }
        else if(TextCoroutine == null)
        {
            TextCoroutine = StartCoroutine(Notice("메뉴가 존재하지 않습니다."));
        }
    }
    void Back()
    {
        menuScroll.SetActive(false);
    }

    IEnumerator Notice(string text)
    {
        Text targetText = NoticePanel.transform.GetChild(0).GetComponent<Text>();

        NoticePanel.gameObject.SetActive(true);
        NoticePanel.DOColor(new Color(1,1,1,0.4f), 0.01f);
        yield return new WaitForSeconds(1);
        targetText.DOText(text, 0.2f);
        NoticePanel.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);

        targetText.text = string.Empty;
        NoticePanel.gameObject.SetActive(false);
    }
}
