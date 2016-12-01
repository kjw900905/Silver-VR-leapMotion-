using UnityEngine;
using System.Collections;

public class MonsterDestroy : MonoBehaviour
{
    public GameObject particle;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(particle, other.transform.position, other.transform.rotation);
        Destroy(other.gameObject);
    }
}
