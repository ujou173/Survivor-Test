using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  public Vector2 inputVec;
  public float speed;

  // 유니티가 만든 스크립트 말고 직접 작성한 스크립트도 컴포넌트처럼 사용 가능
  public Scanner scanner;

  Rigidbody2D rigid;
  SpriteRenderer spriter;
  Animator anim;

  // Start is called before the first frame update
  void Awake()
  {
    rigid = GetComponent<Rigidbody2D>();
    spriter = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();

    // 유니티가 만든 스크립트 말고 직접 작성한 스크립트도 컴포넌트처럼 사용 가능
    scanner = GetComponent<Scanner>();
  }

  void FixedUpdate()
  {
    // // 1. 힘을 준다
    // rigid.AddForce(inputVec);

    // // 2. 속도 제어
    // rigid.velocity = inputVec;

    // // 3. 위치 이동
    // rigid.MovePosition(rigid.position + inputVec);

    Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
    rigid.MovePosition(rigid.position + nextVec);
  }

  void OnMove(InputValue value)
  {
    inputVec = value.Get<Vector2>();
  }

  void LateUpdate()
  {
    anim.SetFloat("Speed", inputVec.magnitude);

    if (inputVec.x != 0)
    {
      spriter.flipX = inputVec.x < 0;
      // flipX 속성은 불리언 값을 입력 받는다. 나는 왼쪽 방향키가 눌렸을 때, 즉 뒤로 갈 때, 다시말해 inputVec.x가 -1 일때 flipX 를 사용할 것이므로
      // true / false 대신에 조건식을 넣어 불리언 값을 반환하도록 하여 코드를 줄일 수 있다.
      // 왼쪽 방향키가 눌리면 inputVec.x는 -1이므로 식을 만족하여 true가 반환 되고, 오른쪽 방향키가 눌리는 동안에는 식이 만족하지 못하여 false가 반환된다.
    }
  }
}
