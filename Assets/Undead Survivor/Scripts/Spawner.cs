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
    level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

    // level이 1일 경우 inspector에서 SpawnData의 첫번째 항목(0.7초의 스폰 10의 체력 등등), level이 2일 경우 두번째 항목으로 자동 전환 됨
    if (timer > spawnData[level].spawnTime)
    {
      timer = 0;
      Spawn();
    }
  }

  void Spawn()
  {
    GameObject enemy = GameManager.instance.pool.Get(0);
    enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    // Enemy의 Init() 함수를 가져와서 사용
    enemy.GetComponent<Enemy>().Init(spawnData[level]);
  }
}

// [System.Serializable] -> inspector에서 인식 할 수 있도록하는 직렬화 과정. 클래스 위에 아래와 같이 작성하면 해당 클래스를 직렬화 한것으로 인식한다.
[System.Serializable]
public class SpawnData
{
  public float spawnTime;
  public int spriteType;
  public int health;
  public float speed;
}
