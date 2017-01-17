using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private GUIStyle progress_empty;
    [SerializeField] private GUIStyle progress_full;
    private bool starved = false;
    private bool stoptime = false;
    [SerializeField] private float currentime;
    [SerializeField] public float maxtime;
    //current progress
    [SerializeField] private float barDisplay;

    private Vector2 pos = new Vector2(500, 732);
    private Vector2 size; //= new Vector2(250, 50);

    [SerializeField] private Texture2D emptyTex;
    [SerializeField] private Texture2D fullTex;

    void Start()
    {
        size = new Vector2(fullTex.width, fullTex.height);
        Assert.IsTrue(currentime >= 0);
        Assert.IsTrue((maxtime >= 0)&&(maxtime >= currentime));
    }


    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y), emptyTex, progress_empty);

        GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), fullTex, progress_full);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);

        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {
        UpdateHungerBar();
    }
    void UpdateHungerBar()
    {
        if (stoptime == false)
        {
            barDisplay = currentime / maxtime;
            currentime -= Time.deltaTime;
            if (currentime <= 0)
            {
                starved = true;
            }
        }
    }
    public void AddTime(float addtime)
    {
        Assert.IsTrue(addtime > 0);
        currentime += addtime;
        if(currentime >= maxtime)
        {
            currentime = maxtime;
        }
    }
    public bool getStarved()
    {
        return starved;
    }
    public void stoptimer()
    {
        stoptime = true;
    }
}
