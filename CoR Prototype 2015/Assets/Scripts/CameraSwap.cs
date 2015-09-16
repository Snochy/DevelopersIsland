using UnityEngine;
using System.Collections;

public class CameraSwap : MonoBehaviour {

    public GameObject BiggerEntity;
    public GameObject SmallerEntity;

    public int currentCharacter = 0;
    public int cameraStat = 0;

    private Vector3 cameraOrgin;
    private float startTime;
    private float journeyLength = 0;

    public void SwapToCharacter(int a)
    {
        currentCharacter = a;
    }

    void Update()
    {
        this.transform.parent.GetComponent<CharacterMotor>().isenabled = false;
        switch(currentCharacter)
        {
            case 0: this.transform.parent = BiggerEntity.transform; break;
            case 1: this.transform.parent = SmallerEntity.transform; break;
        }

        this.transform.localRotation = Quaternion.identity;
        this.transform.parent.GetComponent<CharacterMotor>().isenabled = true;


        if (cameraStat != currentCharacter)
        {
            journeyLength = Vector3.Distance(transform.localPosition, Vector3.zero);
            startTime = Time.time;
            cameraStat = currentCharacter;
        }
        if (transform.localPosition != Vector3.zero)
        {
            float distCovered = (Time.time - startTime) * 5f;
            float fracJourney = distCovered / journeyLength;
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, fracJourney);
        }
    }

    public GameObject GetChild()
    {
        switch (currentCharacter)
        {
            case 0: return BiggerEntity;
            case 1: return SmallerEntity;
            default: return BiggerEntity;
        } 
    }
}
