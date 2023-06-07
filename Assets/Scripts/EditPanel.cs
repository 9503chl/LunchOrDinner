using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPanel : MonoBehaviour
{
    public Toggle[] Toggles;

    public Button[] Buttons;

    public int index;

    public Category category;
    void Start()
    {
        int tempInt = (int)category;

        Toggles[tempInt].isOn = true;

        Toggles[0].onValueChanged.AddListener(delegate { Numbering(0); });
        Toggles[1].onValueChanged.AddListener(delegate { Numbering(1); });
        Toggles[2].onValueChanged.AddListener(delegate { Numbering(2); });
        Toggles[3].onValueChanged.AddListener(delegate { Numbering(3); });
        Toggles[4].onValueChanged.AddListener(delegate { Numbering(4); });
        Toggles[5].onValueChanged.AddListener(delegate { Numbering(5); });

        Buttons[0].onClick.AddListener(Summit);
        Buttons[1].onClick.AddListener(Exit);
    }

    void Numbering(int index)
    {
        category = (Category)index;
    }

    void Summit()
    {
        MenuScroll.Instance.ChangeCategory(index, category);
        gameObject.SetActive(false);
    }
    void Exit()
    {
        gameObject.SetActive(true);
    }
}
