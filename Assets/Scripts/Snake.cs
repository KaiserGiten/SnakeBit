using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {

    Vector2 dir = Vector2.right;
    private GameSystem gameSystem;
    private Healthbar healthbar;
    List<Transform> tail = new List<Transform>();
    private bool ate = false;
    public GameObject tailPrefab;
    public GameObject Explosion;
    [SerializeField] private int Score = 10;
    [SerializeField] private float ExtraTime = 3.0f;
    void Start ()
    {
        Assert.IsNotNull(GameObject.FindWithTag("Game"));
        Assert.IsNotNull(GameObject.FindWithTag("Game").GetComponent<GameSystem>());
        Assert.IsNotNull(GameObject.FindWithTag("Health"));
        Assert.IsTrue(GameObject.FindWithTag("Health").GetComponent<Healthbar>() != null);
        Invariant();

        //GameObject g = (GameObject)Instantiate(tailPrefab, new Vector2(1000,1000), Quaternion.identity);
        //tail.Insert(0, g.transform);

        InvokeRepeating("Move", 0.05f, 0.05f);

        GameObject gameSystemobject = GameObject.FindWithTag("Game");
        gameSystem = gameSystemobject.GetComponent<GameSystem>();


        GameObject healthBarobject = GameObject.FindWithTag("Health");
        healthbar = healthBarobject.GetComponent<Healthbar>();
        Invariant();
        Assert.IsNotNull(healthbar);
        Assert.IsNotNull(gameSystem);
    }

    void Update ()
    {
        if (Input.GetKey(KeyCode.RightArrow) && dir != Vector2.left)
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow) && dir != Vector2.up)
            dir = -Vector2.up;
        else if (Input.GetKey(KeyCode.LeftArrow) && dir != Vector2.right)
            dir = -Vector2.right;
        else if (Input.GetKey(KeyCode.UpArrow) && dir != Vector2.down)
            dir = Vector2.up;
        
        if(healthbar.getStarved())
        {
            SnakeDeath();
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Assert.IsTrue(coll);
        if (coll.CompareTag("food"))
        {
            ate = true;
            Assert.IsTrue(Score >= 0);
            gameSystem.addscore(Score);
            Assert.IsTrue(ExtraTime >= 0);
            healthbar.AddTime(ExtraTime);
            Destroy(coll.gameObject);
        }
        else
        {
            SnakeDeath();
        }
    }

    void Move()
    {
        Vector2 v = transform.position;
        transform.Translate(dir);
        Eat(v);
    }

    public void Eat(Vector2 v)
    {
        if (ate)
        {
            AddSegment(v);
        }

        else if (tail.Count > 0)
        {
            MoveSegment(v);
        }
    }

    public void MoveSegment(Vector2 v)
    {
        tail.Last().position = v;

        tail.Insert(0, tail.Last());
        tail.RemoveAt(tail.Count - 1);
    }

    public void AddSegment(Vector2 v)
    {
        Assert.IsNotNull(tailPrefab);
        GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
        tail.Insert(0, g.transform);
        ate = false;
        Assert.IsTrue(tail.Count > 0);
    }

    public void SnakeDeath()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        gameSystem.gameover();
        healthbar.stoptimer();
        Destroy(gameObject);
    }
    public void Invariant()
    {
        Assert.IsNotNull(tailPrefab, "Prefab is missing");
        Assert.IsNotNull(Explosion, "Explosion Prefab is missing");
    }
}
