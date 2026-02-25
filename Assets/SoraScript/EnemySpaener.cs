using UnityEngine;

public class EnemySpaener : MonoBehaviour
{
    public GameObject EnemmyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(EnemmyPrefab, transform.position, Quaternion.identity);
    }
}
