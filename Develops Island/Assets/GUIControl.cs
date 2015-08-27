using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GUIControl : MonoBehaviour {

	public Button basicBut;
	public Button spikyBut;
	public Button roundBut;
	public Button flatBut;

	public Slider BLengthSlider;
	public Slider BWidthSlider;
	public Slider FLengthSlider;
	public Slider FWidthSlider;

	public SkinnedMeshRenderer blendShape;

	// Use this for initialization
	void Start(){
		basicBut.onClick.AddListener(() => { BasicButtonClick(); });
		spikyBut.onClick.AddListener(() => { SpikyButtonClick(); });
		roundBut.onClick.AddListener(() => { RoundButtonClick(); });
		flatBut.onClick.AddListener(() => { FlatButtonClick(); });
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(FLengthSlider.value > 0)
			blendShape.SetBlendShapeWeight (0, FLengthSlider.value);
		else blendShape.SetBlendShapeWeight (1, FLengthSlider.value * -1);
		if(FWidthSlider.value < 0)
			blendShape.SetBlendShapeWeight (2, FWidthSlider.value * -1);
		else blendShape.SetBlendShapeWeight (3, FWidthSlider.value);
		if(BLengthSlider.value > 0)
			blendShape.SetBlendShapeWeight (4, BLengthSlider.value);
		else blendShape.SetBlendShapeWeight (5, BLengthSlider.value * -1);
		if(BWidthSlider.value > 0)
			blendShape.SetBlendShapeWeight (6, BWidthSlider.value);
		else blendShape.SetBlendShapeWeight (7, BWidthSlider.value * -1);
	}

	void BasicButtonClick ()
	{
		throw new System.NotImplementedException ();
	}

	void SpikyButtonClick ()
	{
		throw new System.NotImplementedException ();
	}

	void RoundButtonClick ()
	{
		throw new System.NotImplementedException ();
	}

	void FlatButtonClick ()
	{
		throw new System.NotImplementedException ();
	}
}
