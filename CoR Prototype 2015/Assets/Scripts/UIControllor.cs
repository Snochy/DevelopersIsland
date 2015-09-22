using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIControllor : MonoBehaviour {

    public Button biggerEntity;
    public Button smallerEntity;

    public Button stationaryButton;
    public Button distanceTracker;
    public Button alongSideFollow;

	public Button stationaryButtonLost;
	public Button seekOutLost;
	public Button roamLost;

	public Text inforText;
	
	public GameObject CameraShift;

	public string CurrentLostString;
	public string CurrentFollowString;

    void Start()
    {
        distanceTracker.onClick.AddListener(() => { distanceAction(); });
        stationaryButton.onClick.AddListener(() => { stationaryAction(); });
        alongSideFollow.onClick.AddListener(() => {alongSideFollowAction();});

		stationaryButtonLost.onClick.AddListener(() => { stationaryButtonLostAction(); });
		seekOutLost.onClick.AddListener(() => { seekOutLostAction(); });
		roamLost.onClick.AddListener(() => { roamLostAction();});
		GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");
		
		foreach (GameObject go in childrenGO) {
			CurrentFollowString = go.GetComponent<AIChildControl> ().currentFollowState.ToString();
			CurrentLostString = go.GetComponent<AIChildControl> ().currentLostState.ToString();
		}
	}
	void Update()
	{
		inforText.text = "Katniss currently: \n" + CurrentLostString + "\n" + CurrentFollowString;
	}
	
	private void bananaAction()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BananaCount--;
    }

    private void stationaryAction()
    {
        GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");

        foreach (GameObject go in childrenGO) {
			go.GetComponent<AIChildControl> ().ChangeFollowState (0);
			go.GetComponent<AIChildControl> ().ClearQue();
			CurrentFollowString = go.GetComponent<AIChildControl> ().currentFollowState.ToString();
		}
    }

    private void distanceAction()
    {
        GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");

        foreach (GameObject go in childrenGO) {
			go.GetComponent<AIChildControl> ().ChangeFollowState (1);
			go.GetComponent<AIChildControl> ().ClearQue ();
			CurrentFollowString = go.GetComponent<AIChildControl> ().currentFollowState.ToString();
		}
	}

    private void alongSideFollowAction()
    {
        GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");

        foreach (GameObject go in childrenGO) {
			go.GetComponent<AIChildControl> ().ChangeFollowState (2);
			go.GetComponent<AIChildControl> ().ClearQue ();
			CurrentFollowString = go.GetComponent<AIChildControl> ().currentFollowState.ToString();
		}
	}

    private void SwitcherAction(int a)
    {
        CameraShift.GetComponent<CameraSwap>().SwapToCharacter(a);
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

	void stationaryButtonLostAction ()
	{
		GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");
		
		foreach (GameObject go in childrenGO) {
			go.GetComponent<AIChildControl> ().ChangeLostState (0);
			go.GetComponent<AIChildControl> ().ClearQue ();
			CurrentLostString = go.GetComponent<AIChildControl> ().currentLostState.ToString();
		}
	}
	
	void seekOutLostAction ()
	{
		GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");
		
		foreach (GameObject go in childrenGO) {
			go.GetComponent<AIChildControl> ().ChangeLostState (1);
			go.GetComponent<AIChildControl> ().ClearQue();
			CurrentLostString = go.GetComponent<AIChildControl> ().currentLostState.ToString();
		}
	}
	
	void roamLostAction ()
	{
		GameObject[] childrenGO = GameObject.FindGameObjectsWithTag("Children");
		
		foreach (GameObject go in childrenGO) {
			go.GetComponent<AIChildControl> ().ChangeLostState (2);
			go.GetComponent<AIChildControl> ().ClearQue();
			CurrentLostString = go.GetComponent<AIChildControl> ().currentLostState.ToString();
		}
	}
}
