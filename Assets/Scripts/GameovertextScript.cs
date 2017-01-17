using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class GameovertextScript : MonoBehaviour
{
    private GUIText text;

    void Start()
    {
        text = gameObject.GetComponent<GUIText>();
        Assert.IsNotNull(text);
    }

    void Update()
    {
        text.color = new Color(Mathf.Sin(Time.time * 5), Mathf.Sin(Time.time * 10), Mathf.Sin(Time.time * 8), 1);
    }
}