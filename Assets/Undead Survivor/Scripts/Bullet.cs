using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    // 원거리 공격 구현
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Init(데미지, 관통력, 방향)
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per >= 0)
        {
            // velocity => 속도
            rigid.velocity = dir * 15f;
        }
    }

    // 관통 관련 함수
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100)
            return;

        per--;

        // 감산 연산 후에 관통이 0보다 작아진다면 오브젝트를 비활성화
        if (per < 0)
        {
            // 비활성화 이전에 미리 물리 속도를 초기화
            rigid.velocity = Vector2.zero;
            // 게임 오브젝트에서 비활성화
            gameObject.SetActive(false);
        }
    }

    // 총알이 맵 밖으로 나갈 시 삭제
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false);
    }
}
