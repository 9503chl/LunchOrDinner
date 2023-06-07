using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    public GameObject DuplicateTarget;

    public GameObject RoulettePiece;

    public GameObject Sphere;

    Color[] Colors = new Color[2] { Color.white, Color.black};

    void OnEnable()
    {
        float angle = 360 / DuplicateTarget.transform.childCount;
        for (int i = 0; i < DuplicateTarget.transform.childCount; i++)
        {
            GameObject Piece = Instantiate(RoulettePiece, Vector2.zero, Quaternion.Euler(0, 180, angle * i), transform);
            Piece.transform.localPosition = Vector2.zero;

            Image TempImage = Piece.GetComponent<Image>();
            TempImage.color = Colors[i % 2];
            TempImage.fillAmount = angle / 360;

            Transform TempTr = Piece.transform.GetChild(0).transform;//Sphere

            Vector2 TempVector = TempTr.transform.localPosition;

            TempVector = Quaternion.Euler(0, 0, -angle / 2) * TempVector;

            Text TempText = Piece.GetComponentInChildren<Text>();
            TempText.text = DuplicateTarget.transform.GetChild(i).GetChild(1).GetComponent<Text>().text;
            TempText.color = Colors[(i + 1) % 2];
            TempText.transform.Translate(TempVector/ 2);//지역좌표 쓸때 좋음
            TempText.transform.Rotate(0, 0, -angle / 2);

            TempVector = Quaternion.Euler(0, 0, -angle / 2) * TempVector;

            Instantiate(Sphere, TempVector, Quaternion.Euler(0, 0, 0), Piece.transform);
        }
    }

    void Spin()
    {

    }
}
