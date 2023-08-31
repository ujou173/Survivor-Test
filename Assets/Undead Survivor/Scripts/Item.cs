using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            // 근접과 원거리 무기는 동일한 로직을 사용할 것이기 때문에 붙여서 사용. 다수의 case문을 붙여서 로직을 실행 할 수 있음
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:

                break;
            case ItemData.ItemType.Glove:

                break;
            case ItemData.ItemType.Shoe:

                break;
            case ItemData.ItemType.Heal:

                break;
        }

        level++;

        // 최대 레벨 도달 시 버튼 클릭 제거
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
