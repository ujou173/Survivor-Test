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

    // 굳이 타이머를 public으로 만들 필요는 없어서 지역변수로 선언
    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
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
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // 테스트용 코드
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 3);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            WeaponPosition();
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        // id와 prefabsId를 맞추기 위한 for문
        // 스크립트블 오브젝트의 독립성을 위해서 인덱스가 아닌 프리펩을 이용
        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = -150;
                WeaponPosition();
                break;

            default:
                // 원거리 무기 발사를 timer와 speed를 이용해서 하므로, 여기서 speed는 연사 속도가 됨
                speed = 0.3f;
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


            // Init(데미지, 관통)에서 -100은 무한 관통을 의미, 음수(-1 이하)면 무한 관통
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero);
        }
    }

    void Fire()
    {
        // 스캐너에 걸려있는 nearestTarget이 null이라면 (즉, 스캐너 범위 안에 enemy가 없다면) // null일 경우 false이지만 !가 붙어서 true가 된다
        if (!player.scanner.nearestTarget)
            return;

        // 총알이 나가야 하는 방향 설정
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        // 회전은 Vector가 아닌 Quaternion을 이용 / FromToRotation(축, 목표) -> 지정된 축을 중심으로 목표를 항해 회전하는 함수
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
