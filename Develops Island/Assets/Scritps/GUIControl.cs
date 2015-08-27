using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GUIControl : MonoBehaviour {

	public Button basicBut;
	public Button spikyBut;
	public Button roundBut;
	public Button flatBut;

	public Button nextBut;

	public Slider BLengthSlider;
	public Slider BWidthSlider;
	public Slider FLengthSlider;
	public Slider FWidthSlider;
	public Slider TextureSlider;

	public SkinnedMeshRenderer blendShape;
	public SkinnedMeshRenderer finBlendShape;
	public SkinnedMeshRenderer spikyBlendShape;
	public SkinnedMeshRenderer flatBlendShape;
	public SkinnedMeshRenderer roundBlendShape;

	public GameObject basicFin;
	public GameObject spikyFin;
	public GameObject roundFin;
	public GameObject flatFin;

	public Material material01;
	public Material material02;
	public Material material03;
	public Material material04;
	
	// Use this for initialization
	void Start(){
		basicBut.onClick.AddListener(() => { BasicButtonClick(); });
		spikyBut.onClick.AddListener(() => { SpikyButtonClick(); });
		roundBut.onClick.AddListener(() => { RoundButtonClick(); });
		flatBut.onClick.AddListener(() => { FlatButtonClick(); });
		nextBut.onClick.AddListener(() => { BackButtonClick(); });
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch ((int)TextureSlider.value)
		{
		case 0:
			blendShape.material = material01;
			break;
		case 1:
			blendShape.material = material02;
			break;
		case 2:
			blendShape.material = material03;
			break;
		case 3:
			blendShape.material = material04;
			break;
		default:
			blendShape.material = material01;
			break;
		}

		if (FLengthSlider.value > 0) 
		{
			blendShape.SetBlendShapeWeight (0, FLengthSlider.value);
			finBlendShape.SetBlendShapeWeight (0, FLengthSlider.value);
			spikyBlendShape.SetBlendShapeWeight (0, FLengthSlider.value);
			roundBlendShape.SetBlendShapeWeight (0, FLengthSlider.value);
			flatBlendShape.SetBlendShapeWeight (0, FLengthSlider.value);
		} 
		else 
		{
			blendShape.SetBlendShapeWeight (1, FLengthSlider.value * -1);
			finBlendShape.SetBlendShapeWeight (1, FLengthSlider.value * -1);
			spikyBlendShape.SetBlendShapeWeight (1, FLengthSlider.value * -1);
			roundBlendShape.SetBlendShapeWeight (1, FLengthSlider.value * -1);
			flatBlendShape.SetBlendShapeWeight (1, FLengthSlider.value * -1);
		}
		if (FWidthSlider.value < 0) 
		{
			blendShape.SetBlendShapeWeight (2, FWidthSlider.value * -1);
			finBlendShape.SetBlendShapeWeight (2, FWidthSlider.value * -1);
			spikyBlendShape.SetBlendShapeWeight (2, FWidthSlider.value * -1);
			roundBlendShape.SetBlendShapeWeight (2, FWidthSlider.value * -1);
			flatBlendShape.SetBlendShapeWeight (2, FWidthSlider.value * -1);
		} 
		else 
		{
			blendShape.SetBlendShapeWeight (3, FWidthSlider.value);
			finBlendShape.SetBlendShapeWeight (3, FWidthSlider.value);
			spikyBlendShape.SetBlendShapeWeight (3, FWidthSlider.value);
			roundBlendShape.SetBlendShapeWeight (3, FWidthSlider.value);
			flatBlendShape.SetBlendShapeWeight (3, FWidthSlider.value);
		}
		if (BLengthSlider.value > 0) {
			blendShape.SetBlendShapeWeight (4, BLengthSlider.value);
		} 
		else 
		{
			blendShape.SetBlendShapeWeight (5, BLengthSlider.value * -1);
		}
		if (BWidthSlider.value > 0) {
			blendShape.SetBlendShapeWeight (6, BWidthSlider.value);
		} 
		else 
		{
			blendShape.SetBlendShapeWeight (7, BWidthSlider.value * -1);
		}
	}

	void BasicButtonClick ()
	{
		basicFin.SetActive (true);
		spikyFin.SetActive (false);
		roundFin.SetActive (false);
		flatFin.SetActive (false);
	}

	void SpikyButtonClick ()
	{
		basicFin.SetActive (false);
		spikyFin.SetActive (true);
		roundFin.SetActive (false);
		flatFin.SetActive (false);
	}

	void RoundButtonClick ()
	{
		basicFin.SetActive (false);
		spikyFin.SetActive (false);
		roundFin.SetActive (true);
		flatFin.SetActive (false);
	}

	void FlatButtonClick ()
	{
		basicFin.SetActive (false);
		spikyFin.SetActive (false);
		roundFin.SetActive (false);
		flatFin.SetActive (true);
	}

	void BackButtonClick ()
	{
		Application.LoadLevel(1);
	}
}
