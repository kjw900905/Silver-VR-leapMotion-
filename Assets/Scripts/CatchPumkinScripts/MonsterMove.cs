using UnityEngine;
using System.Collections;

public class MonsterMove : MonoBehaviour
{
    public float animationTime;

    // Use this for initialization
    void Start()
    {
        animationTime = Random.Range(-3f, 3f);
    }

    void Update()
    {
        monsterMove();
    }

    void monsterMove()
    {
        this.animationTime -= Time.deltaTime;

        if (this.animationTime > 0)
        {

            this.gameObject.transform.Translate(Vector3.forward * (float)0.005);
        }
        else if (this.animationTime < 0 && this.animationTime >= -3f)
        {
            this.gameObject.transform.Translate(Vector3.back * (float)0.005);
        }
        else
            this.animationTime = 3f;
    }
}
