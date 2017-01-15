using UnityEngine;
using UnityEngine.SceneManagement;

public class KillEnemy : MonoBehaviour
{
    private void Start()
    {
        GetComponent<GetKilled>().die += EnemyDies;
    }

    private void EnemyDies()
    {
        Destroy(transform.parent.gameObject);
    }
}