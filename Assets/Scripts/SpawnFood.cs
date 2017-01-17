using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class SpawnFood : MonoBehaviour {

    public GameObject foodPrefab;

    public Transform borderTop;
    public Transform borderBot;
    public Transform borderLeft;
    public Transform borderRight;

    [SerializeField] private float SpawnRate = 3.0f;

    void Start ()
    {
        Assert.IsTrue(SpawnRate > 0, "SpawnRate must be higher then 0");
        InvokeRepeating("Spawn", 0.5f, SpawnRate);
	}

    void Spawn()
    {
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);

        int y = (int)Random.Range(borderBot.position.y, borderTop.position.y);

        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }
}
