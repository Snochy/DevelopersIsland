using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public GameObject currentChild;
    public int BananaCount = 0;

    public bool isOverButton;

    void Update()
    {
        EventSystem eventsystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (eventsystem.IsPointerOverGameObject())
        {
            isOverButton = true;
        }
        else isOverButton = false;
    }
}
