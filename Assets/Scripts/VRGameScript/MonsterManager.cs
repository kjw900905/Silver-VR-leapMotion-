using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject monsterPrefab;
    public GameObject myCamera;
    public List<GameObject> monsterList;

    public float createTime = 2.0f;
    public int maxMonster = 9;
    public bool isGameOver = false;

    void Start()
    {
        monsterList = new List<GameObject>();
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        if(points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());
        }
    }
	
    IEnumerator CreateMonster()
    {
        GameObject monster;

        while (!isGameOver)
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("MONSTER").Length;

            if(monsterCount < maxMonster)
            {
                yield return new WaitForSeconds(createTime);

                //Vector3 v;
                int idx = Random.Range(1, points.Length);
                
                monster = (GameObject)Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
                monsterList.Add(monster);

                monster.transform.LookAt(myCamera.transform.position);
                monster.transform.Rotate(-90, 0, 0);
                //v = monster.transform.position;
                //v.y = 187;
                //monster.transform.position = v;
            }
            else
            {
                yield return null;
            }
        }
    }
}
