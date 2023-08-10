using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        // 테스트용 코드
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            WeaponPosition();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                WeaponPosition();
                break;
            default:
                break;
        }
    }

    void WeaponPosition()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            // 기존: 항상 ObjectPool에서 prefabs를 가져옴
            // 변경: 기존 오브젝트를 먼저 활용하고 모자란 것만 ObjectPool에서 prefabs를 가져옴
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            // 레벨업 등으로 무기 추가 시 위치 초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // 위치 조절
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);


            // Init(데미지, 관통)에서 -1은 무한 관통을 의미
            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
    }
}
