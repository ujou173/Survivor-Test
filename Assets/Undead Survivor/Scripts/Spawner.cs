using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

  public Transform[] spawnPoint;
  public SpawnData[] spawnData;

  int level;
  float timer;

  void Awake()
  {
    spawnPoint = GetComponentsInChildren<Transform>();
  }

  void Update()
  {

    timer += Time.deltaTime;
    level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

    if (timer > (level == 0 ? 0.5f : 0.2f))
    {
      timer = 0;
      Spawn();
    }
  }

  void Spawn()
  {
    GameObject enemy = GameManager.instance.pool.Get(level);
    enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
  }
}

// [System.Serializable] -> inspector에서 인식 할 수 있도록하는 직렬화 과정. 클래스 위에 아래와 같이 작성하면 해당 클래스를 직렬화 한것으로 인식한다.
[System.Serializable]
public class SpawnData
{
  public int spriteType;
  public float spawnTime;
  public int health;
  public float speed;
}
