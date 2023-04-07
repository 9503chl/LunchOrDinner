using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonClick : MonoBehaviour//글자제한 걸어야함.
{
    [SerializeField] Menu m_menu;

    [SerializeField] Toggle[] CategoryToggles;

    [SerializeField] Button SlotOpen;

    [SerializeField] Button CommitButton;

    [SerializeField] Button RevokeButton;

    [SerializeField] Button SearchButton;

    [SerializeField] Button BackButton;

    [SerializeField] Text[] CommitInput;

    [SerializeField] Text RevokeInput;

    [SerializeField] Image NoticePanel;

    [SerializeField] GameObject SlotMachine;

    Category category;

    MenuScroll ms;

    Coroutine TextCoroutine;
    void Start()
    {
        CommitButton.onClick.AddListener(CommitMenu);
        RevokeButton.onClick.AddListener(RevokeMenu);
        SearchButton.onClick.AddListener(SearchMenu);
        SlotOpen.onClick.AddListener(OpenSlot);
        BackButton.onClick.AddListener(Back);

        CategoryToggles[0].onValueChanged.AddListener(delegate { CategoryToggle(0); });
        CategoryToggles[1].onValueChanged.AddListener(delegate { CategoryToggle(1); });
        CategoryToggles[2].onValueChanged.AddListener(delegate { CategoryToggle(2); });
        CategoryToggles[3].onValueChanged.AddListener(delegate { CategoryToggle(3); });
        CategoryToggles[4].onValueChanged.AddListener(delegate { CategoryToggle(4); });
        CategoryToggles[5].onValueChanged.AddListener(delegate { CategoryToggle(5); });

        ms = FindObjectOfType<MenuScroll>();
    }
    void OpenSlot()
    {
        SlotMachine.SetActive(true);
    }
    void CommitMenu()
    {
        if (!m_menu.menu.Contains(CommitInput[0].text))
        {
            m_menu.menu.Add(CommitInput[0].text);
            m_menu.info.Add(CommitInput[1].text);
            m_menu.category.Add(category);
            m_menu.scope.Add(int.Parse(CommitInput[2].text));
            ms.Add(m_menu.menu.Count-1);
        }
        else if(TextCoroutine == null)
        {
            TextCoroutine = StartCoroutine(Notice("메뉴가 이미 존재합니다."));
        }
    }
    void RevokeMenu()
    {
        if (m_menu.menu.Contains(RevokeInput.text))
        {
            int index = m_menu.menu.IndexOf(RevokeInput.text);
            ms.Remove(index);
            m_menu.menu.Remove(RevokeInput.text);
            m_menu.info.RemoveAt(index);
            m_menu.category.RemoveAt(index);
            m_menu.scope.RemoveAt(index);
        }
        else if (TextCoroutine == null)
        {
            TextCoroutine = StartCoroutine(Notice("메뉴가 존재하지 않습니다."));
        }
    }
    void CategoryToggle(int index)
    {
        category = (Category)index;
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
         Application.Quit();
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
