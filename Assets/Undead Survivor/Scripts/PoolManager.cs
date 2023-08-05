using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
  // 1. 프리펩들을 보관한 변수
  // 2. 풀 담당을 하는 리스트들
  // 프리펩을 보관하는 변수와 풀을 담당하는 리스트는 1:1 관계. 변수가 두개일 경우 리스트도 두개 필요

  // 프레핍들을 보관할 변수(하나가 아닐 수 있으므로 배열 형태로 선언)
  public GameObject[] prefabs;

  // 풀 담당을 하는 리스트들(마찬가지로 여러개일 수 있으므로 배열 형태로 선언)
  List<GameObject>[] pools;

  void Awake()
  {
    pools = new List<GameObject>[prefabs.Length];

    for (int index = 0; index < pools.Length; index++)
    {
      pools[index] = new List<GameObject>();
    }
  }

  public GameObject Get(int index)
  {
    GameObject select = null;

    // 선택한 풀의 놀고 있는(비활성화 된) 게임오브젝트가 있을 경우 select 변수에 할당
    foreach (GameObject item in pools[index])
    {
      if (!item.activeSelf)
      {
        select = item;
        select.SetActive(true);
        break;
      }
    }
    // 비활성화 된 게임오브젝트가 없을 경우 새롭게 생성해서 select에 할당
    if (!select)
    {
      select = Instantiate(prefabs[index], transform);
      pools[index].Add(select);
    }
    return select;
  }
}
