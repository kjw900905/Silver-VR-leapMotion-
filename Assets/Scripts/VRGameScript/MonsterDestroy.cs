using UnityEngine;
using System.Collections;

public class MonsterDestroy : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
