using UnityEngine;
using UnityEngine.SceneManagement;

public class KillEnemy : MonoBehaviour
{
    private void Start()
    {
        GetComponent<GetKilled>().Die += EnemyDies;
    }

    private void EnemyDies()
    {
        Destroy(transform.parent.gameObject);
    }
}