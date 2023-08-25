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
                // text는 문자열값만 들어갈 수 있다.
                // Format("포맷의 타입", 포맷을 적용 할 데이터) / "Lv.{0:F0}" => Lv. 이라는 텍스트와 {데이터의 index 순번(GameManager.instance.level): 데이터 포맷(F0 -> 소수점 0번째 자리까지)}
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                // Format의 D는 자리수를 지정하는 타입 -> D2는 두자리를 고정하라는 의미
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:

                break;
        }
    }
}
