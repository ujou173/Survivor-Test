using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Text를 사용하기 위해 추가되는 클래스
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // enum -> 열겨형
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    // 연산이 다 되고 끝나고나서 실행 LateUpdate()
    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:

                break;
            case InfoType.Kill:

                break;
            case InfoType.Time:

                break;
            case InfoType.Health:

                break;
        }
    }
}
