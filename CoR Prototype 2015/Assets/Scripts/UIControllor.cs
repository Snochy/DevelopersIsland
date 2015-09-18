using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIControllor : MonoBehaviour {

    public Button biggerEntity;
    public Button smallerEntity;

    public Button stationaryButton;
    public Button distanceTracker;

    public GameObject CameraShift;

    void Start()
    {
        distanceTracker.onClick.AddListener(() => { distanceAction(); });
        stationaryButton.onClick.AddListener(() => { stationaryAction(); });
    }

    private void bananaAction()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BananaCount--;
    }

    private void stationaryAction()
    {
        GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");

        foreach (GameObject go in childrenGO)
            go.GetComponent<AIChildControl>().ChangeState(0);
    }

    private void distanceAction()
    {
        GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");

        foreach (GameObject go in childrenGO)
            go.GetComponent<AIChildControl>().ChangeState(1);
    }

    private void SwitcherAction(int a)
    {
        CameraShift.GetComponent<CameraSwap>().SwapToCharacter(a);
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
