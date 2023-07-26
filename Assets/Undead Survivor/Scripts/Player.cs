using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  public Vector2 inputVec;
  public float speed;

  Rigidbody2D rigid;

  // Start is called before the first frame update
  void Awake()
  {
    rigid = GetComponent<Rigidbody2D>();
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
}
