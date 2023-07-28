using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
  void OnTriggerExit2D(Collider2D collision)
  {
    if (!collision.CompareTag("Area"))
      return;

    Vector3 PlayerPos = GameManager.instance.player.transform.position;
    Vector3 myPos = transform.position;

    float dirX = PlayerPos.x - myPos.x;
    float dirY = PlayerPos.y - myPos.y;

    float diffX = Mathf.Abs(dirX);
    float diffY = Mathf.Abs(dirY);

    dirX = dirX > 0 ? 1 : -1;
    dirY = dirY > 0 ? 1 : -1;

    // // Mathf.Abs() => 괄호 안의 값을 절대값으로 만드는 함수
    // float diffX = Mathf.Abs(PlayerPos.x - myPos.x);
    // float diffY = Mathf.Abs(PlayerPos.y - myPos.y);

    // // inputSystem 사용시 캐릭터 방향을 알기 위한 로직
    // Vector3 playerDir = GameManager.instance.player.inputVec;
    // float dirX = playerDir.x < 0 ? -1 : 1;
    // float dirY = playerDir.y < 0 ? -1 : 1;

    switch (transform.tag)
    {
      case "Ground":
        // player가 X축 이동을 할 때
        if (diffX > diffY)
        {
          transform.Translate(Vector3.right * dirX * 60);
        }
        // player가 Y축 이동을 할 때
        else if (diffX < diffY)
        {
          transform.Translate(Vector3.up * dirY * 40);
        }
        break;
      case "Enemy":

        break;
    }
  }
}
