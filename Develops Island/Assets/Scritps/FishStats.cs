using UnityEngine;
using System.Collections;

public class FishStats : MonoBehaviour {

	public SkinnedMeshRenderer blendShape;
	public SkinnedMeshRenderer finBlendShape;
	public SkinnedMeshRenderer spikyBlendShape;
	public SkinnedMeshRenderer flatBlendShape;
	public SkinnedMeshRenderer roundBlendShape;
	
	public GameObject basicFin;
	public GameObject spikyFin;
	public GameObject roundFin;
	public GameObject flatFin;

	public int finInt;
	public float FLengthInt;
	public float FWidthInt;
	public float BLengthInt;
	public float BWidthInt;
	public int textureInt;

	public float lifeSpan = 10f;
	private float timer = 0;

	public Material material01;
	public Material material02;
	public Material material03;
	public Material material04;
	
	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= lifeSpan)
			Destroy (this.gameObject);
		if (FLengthInt > 0) 
		{
			blendShape.SetBlendShapeWeight (0, FLengthInt);
			finBlendShape.SetBlendShapeWeight (0, FLengthInt);
			spikyBlendShape.SetBlendShapeWeight (0, FLengthInt);
			roundBlendShape.SetBlendShapeWeight (0, FLengthInt);
			flatBlendShape.SetBlendShapeWeight (0, FLengthInt);
		} 
		else 
		{
			blendShape.SetBlendShapeWeight (1, FLengthInt * -1);
			finBlendShape.SetBlendShapeWeight (1, FLengthInt * -1);
			spikyBlendShape.SetBlendShapeWeight (1, FLengthInt * -1);
			roundBlendShape.SetBlendShapeWeight (1, FLengthInt * -1);
			flatBlendShape.SetBlendShapeWeight (1, FLengthInt * -1);
		}
		if (FWidthInt < 0) 
		{
			blendShape.SetBlendShapeWeight (2, FWidthInt * -1);
			finBlendShape.SetBlendShapeWeight (2, FWidthInt * -1);
			spikyBlendShape.SetBlendShapeWeight (2, FWidthInt * -1);
			roundBlendShape.SetBlendShapeWeight (2, FWidthInt * -1);
			flatBlendShape.SetBlendShapeWeight (2, FWidthInt * -1);
		} 
		else 
		{
			blendShape.SetBlendShapeWeight (3, FWidthInt);
			finBlendShape.SetBlendShapeWeight (3, FWidthInt);
			spikyBlendShape.SetBlendShapeWeight (3, FWidthInt);
			roundBlendShape.SetBlendShapeWeight (3, FWidthInt);
			flatBlendShape.SetBlendShapeWeight (3, FWidthInt);
		}
		if (BLengthInt > 0) {
			blendShape.SetBlendShapeWeight (4, BLengthInt);
		} 
		else 
		{
			blendShape.SetBlendShapeWeight (5, BLengthInt * -1);
		}
		if (BWidthInt > 0) {
			blendShape.SetBlendShapeWeight (6, BWidthInt);
		} 
		else 
		{
			blendShape.SetBlendShapeWeight (7, BWidthInt * -1);
		}

		switch (finInt)
		{
		case 0:
			basicFin.SetActive (true);
			spikyFin.SetActive (false);
			roundFin.SetActive (false);
			flatFin.SetActive (false);
			break;
		case 1:
			basicFin.SetActive (false);
			spikyFin.SetActive (true);
			roundFin.SetActive (false);
			flatFin.SetActive (false);
			break;
		case 2:
			basicFin.SetActive (false);
			spikyFin.SetActive (false);
			roundFin.SetActive (false);
			flatFin.SetActive (true);
			break;
		case 3:
			basicFin.SetActive (false);
			spikyFin.SetActive (false);
			roundFin.SetActive (true);
			flatFin.SetActive (false);
			break;
		default:
			basicFin.SetActive (true);
			spikyFin.SetActive (false);
			roundFin.SetActive (false);
			flatFin.SetActive (false);
			break;
		}

		switch (textureInt)
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
		
		transform.Translate (0, 0, .1f);
	}
}
