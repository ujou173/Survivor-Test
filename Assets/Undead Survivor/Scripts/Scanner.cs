using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // 스캔 할 범위 값
    public float sacnRange;

    // 스캔 할 대상(유니티의 레이어 이용)
    public LayerMask targetLayer;

    // 스캔 결과값 배열
    public RaycastHit2D[] targets;

    // 가장 가까운 목표
    public Transform nearestTarget;

    void FixedUpdate()
    {
        // CircleCastAll => 원형 범위로 검색 // CircleCasetAll(캐스팅 시작 위치, 원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어)
        targets = Physics2D.CircleCastAll(transform.position, sacnRange, Vector2.zero, 0, targetLayer);
    }
}
