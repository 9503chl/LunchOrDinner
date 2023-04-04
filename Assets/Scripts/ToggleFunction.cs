using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFunction: MonoBehaviour
{
    [SerializeField] MenuScroll menuScroll;

    [SerializeField] Toggle[] ViewToggle;

    bool isToggleAll = false;
    bool isTogglePrivate = false;

    int count = 5;
    void Start()
    {
        ViewToggle[0].onValueChanged.AddListener(
            delegate { ToggleOn(ViewToggle[0].isOn, menuScroll.menuContents, ViewToggle);});

        ViewToggle[1].onValueChanged.AddListener(
            delegate { ToggleOn(ViewToggle[1].isOn, menuScroll.SpicyMenu); });

        ViewToggle[2].onValueChanged.AddListener(
            delegate { ToggleOn(ViewToggle[2].isOn, menuScroll.CrispyMenu); });

        ViewToggle[3].onValueChanged.AddListener(
            delegate { ToggleOn(ViewToggle[3].isOn, menuScroll.NotCravingMenu); });

        ViewToggle[4].onValueChanged.AddListener(
            delegate { ToggleOn(ViewToggle[4].isOn, menuScroll.TooFarMenu); });

        ViewToggle[5].onValueChanged.AddListener(
            delegate { ToggleOn(ViewToggle[5].isOn, menuScroll.WaitingMenu); });
    }
    void ToggleOn(bool isOn, List<GameObject> lists, Toggle[] toggles)
    {
        isToggleAll = true;
        if (!isTogglePrivate)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                lists[i].SetActive(!isOn);
            }
            for (int i = 1; i < toggles.Length; i++)
            {
                toggles[i].isOn = isOn;
            }
            count = isOn ? 5 : 0;
        }

        isToggleAll = false;
    }
    void ToggleOn(bool isOn, List<GameObject> lists)//다시 켜질때 전체선택이 켜져야함.
    {
        isTogglePrivate = true;
        if (!isToggleAll)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                lists[i].SetActive(!isOn);
            }
            count = isOn ? ++count : --count;
            bool AllToggle = count == 5 ? true : false;
            ViewToggle[0].isOn = AllToggle;
        }
        isTogglePrivate = false;
    }
}