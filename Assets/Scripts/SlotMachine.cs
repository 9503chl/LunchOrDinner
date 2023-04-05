using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] GameObject DuplicateTarget;

    [SerializeField] GameObject Slot;//안에 내용물들

    void OnEnable()
    {
        Transform obj = DuplicateTarget.transform;
        for (int i = 0; i < obj.childCount; i++)
        {
            if (obj.GetChild(i).gameObject.activeSelf)
            {
                Instantiate(obj.GetChild(i).gameObject, Slot.transform);
            }
        }
        StartCoroutine(StartSlot());
    }

    IEnumerator StartSlot()
    {
        float speed = 0;
        int cnt = DuplicateTarget.transform.childCount;
        yield return new WaitForSeconds(0.3f);
        for(int i = 0; i < cnt * 160; i++)
        {
            Slot.transform.localPosition -= new Vector3(0,5f,0);
            if (Slot.transform.localPosition.y < 5f)
            {
                Slot.transform.localPosition += new Vector3(0, 100f * (cnt / 2) + 1, 0);
            }
            speed += 0.00002f;
            Mathf.Clamp(speed, 0, 0.0002f);
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
