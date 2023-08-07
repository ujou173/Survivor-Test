using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  // 플레이어를 추적할 속도
  public float speed;

  // 현재 체력
  public float health;

  // 최대 체력
  public float maxHealth;

  // 스프라이트, animator를 바꾸기 위한 컨트롤러 선언 / 스프라이트가 여러개일 수 있으므로 배열로 선언
  public RuntimeAnimatorController[] animCon;

  // 추적할 대상(플레이어), 물리를 사용해서 추적할 것이기 때문에 Rigidbody2D 사용
  public Rigidbody2D target;

  // 몬스터가 살아있는지 죽어있는지 판별하기 위한 bool 변수
  bool isLive;

  Rigidbody2D rigid;
  Animator anim;
  SpriteRenderer spriter;


  void Awake()
  {
    rigid = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    spriter = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (!isLive)
      return;

    // Enemy가 움직일 방향 => 플레이어의 위치와 Enemy의 위치의 차이를 이용해 방향을 구함
    Vector2 dirVec = target.position - rigid.position;
    Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
    rigid.MovePosition(rigid.position + nextVec);
    // 충돌에 의한 물리 속도가 이동에 영향을 주지 않도록 값을 0으로 고정
    rigid.velocity = Vector2.zero;
  }

  void LateUpdate()
  {
    if (!isLive)
      return;
    // Enemy가 플레이어의 위치에 따라 바라보는 방향이 바뀌도록 설정
    // Enemy의 x위치가 플레이어의 x위치보다 크면 뒤를 돌아보도록 구현
    spriter.flipX = target.position.x < rigid.position.x;
  }

  void OnEnable()
  {
    target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    isLive = true;
    health = maxHealth;
  }

  // 레벨링에 따른 enemy의 상태를 직접 컨트롤하기 위한 함수
  public void Init(SpawnData data)
  {
    anim.runtimeAnimatorController = animCon[data.spriteType];
    speed = data.speed;
    maxHealth = data.health;
    health = data.health;
  }
}
