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

    switch (transform.tag)
    {
      case "Ground":
        float diffX = PlayerPos.x - myPos.x;
        float diffY = PlayerPos.y - myPos.y;
        float dirX = diffX < 0 ? -1 : 1;
        float dirY = diffY < 0 ? -1 : 1;

        diffX = Mathf.Abs(diffX);
        diffY = Mathf.Abs(diffY);

        // player가 X축 이동을 할 때
        if (diffX > diffY)
        {
          transform.Translate(Vector3.right * dirX * 50);
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
