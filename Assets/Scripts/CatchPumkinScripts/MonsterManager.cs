using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MonsterManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject monsterPrefab;
    public GameObject myCamera;
    public List<GameObject> monsterList;
    public List<int> monsterSeed;
    //public GameObject particle;

    int monsterCount;
    int score;
    int totalScore;
    string startTime;
    string endTime;
    string contentsName;
    float time;
    bool isStopGame;

    int randomNum;
    float createTime = 2.0f;
    int maxMonster = 9;
    bool isGameOver = false;

    public Canvas dialogueCanvas;
    public Canvas gamePlayUI;

    public Text levelText;
    public Text timeText;
    public Text pointText;
    public Text scoreMessage;
    public Text dialogMessage;


    void Start()
    {
        monsterList = new List<GameObject>();
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        /*if(points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());
        }*/

        startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        contentsName = "호박 잡기 게임";
        monsterCount = 1;
        dialogMessage.text = "시작\n 나오는 모든 호박을 잡아주시기 바랍니다.\n";
        dialogueCanvas.enabled = true;
        gamePlayUI.enabled = false;
        isStopGame = true;
    }

    /*IEnumerator CreateMonster()
    {
        GameObject monster;

        while (!isGameOver)
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("MONSTER").Length;

            if(monsterCount < maxMonster)
            {
                yield return new WaitForSeconds(createTime);

                //Vector3 v;
                int idx = UnityEngine.Random.Range(1, points.Length);
                
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
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isStopGame)
            return;
        CheckTime();
        //MonsterMove();
        DrawGameInfo();
        CheckMonsterCount();
    }

    /*void MonsterMove()
    {
        //animationTime = Random.Range(-3f, 3f);
        //animationTime -= Time.deltaTime;

        for (int i = 0; i < monsterList.Count; i++)
        {
            //UnityEngine.Debug.Log(i + " "  + monsterList[i].GetComponent<MonsterAnimationTime>().animationTime);
            monsterList[i].GetComponent<MonsterAnimationTime>().animationTime -= Time.deltaTime;
            if (monsterList[i].GetComponent<MonsterAnimationTime>().animationTime > 0)
            {

                monsterList[i].transform.Translate(Vector3.forward * (float)0.005);
            }
            else if (monsterList[i].GetComponent<MonsterAnimationTime>().animationTime < 0 && monsterList[i].GetComponent<MonsterAnimationTime>().animationTime >= -3f)
            {
                monsterList[i].transform.Translate(Vector3.back * (float)0.005);
            }
            else
                monsterList[i].GetComponent<MonsterAnimationTime>().animationTime = 3f;

            monsterList[i].transform.LookAt(myCamera.transform.position);
            monsterList[i].transform.Rotate(-90, 0, 0);
        }
    }*/

    void InitGame()
    {
        score = 0;
        time = 120f;
        MakeMonster(monsterCount);
        levelText.text = "Level: " + monsterCount;
        pointText.text = "Score : " + score;
        isStopGame = false;
    }

    void CheckTime()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 0;
            FinishGame(false);
        }
    }

    void DrawGameInfo()
    {
        timeText.text = "Time : " + (int)time;
    }

    void CheckMonsterCount()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i] == null)
            {
                
                monsterList.RemoveAt(i);
                score += 10;
                totalScore += score;
                pointText.text = "Score : " + score;
            }
        }

        if (monsterList.Count == 0)
        {
            FinishGame(true);
        }

    }

    void FinishGame(bool isSucceed)
    {
        isStopGame = true;
        foreach (GameObject cube in monsterList)
            Destroy(cube);
        monsterList.Clear();

        dialogMessage.text = "";
        scoreMessage.text = "";

        if (isSucceed)
        {
            GameObject.Find("DialogueCanvas").GetComponentInChildren<ChangeImage>().changeImage(isSucceed);
            scoreMessage.text = score.ToString();
            monsterCount++;
        }
        else
        {
            GameObject.Find("DialogueCanvas").GetComponentInChildren<ChangeImage>().changeImage(isSucceed);
            scoreMessage.text = score.ToString();
        }

        dialogueCanvas.enabled = true;
        gamePlayUI.enabled = false;
    }

    void MakeMonster(int count)
    {
        GameObject monster;
       
        for (int i=0; i<count; i++)
        {
            int idx = UnityEngine.Random.Range(1, points.Length);
           
            monster = (GameObject)Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
            monsterList.Add(monster);

            monster.transform.LookAt(myCamera.transform.position);
            monster.transform.Rotate(-90, 0, 0);
        }
    }


    public void OnPlayButton()
    {
        dialogueCanvas.enabled = false;
        gamePlayUI.enabled = true;
        InitGame();
    }

    public void OnPlayEndButton()
    {
        //게임 기록 데이터 정리 및 전송..
        endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string cubeCount = this.monsterCount.ToString();
        string totalScore = this.totalScore.ToString();

        if (score != 0)
        {
            PlayRecordData data = new PlayRecordData(GameStatusModel.trainee.getId(), GameStatusModel.assistant.id,
                                                this.contentsName, cubeCount, totalScore, this.startTime, this.endTime);

            PlayRecordDataServiceManager.SendPlayRecordData(data);
        }

        SceneManager.LoadScene("MainMenuScene2");
    }

    public void OnGameStopButton()
    {
        FinishGame(false);
    }
}
