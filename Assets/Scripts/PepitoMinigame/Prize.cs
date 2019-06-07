using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    public const string pathResources = "Prizes/";

    public PrizeType prizeType;

    public Transform posCoin;

    private Vector3 vRotation = Vector3.up;
    private float velRotation = 0;
    public bool isRotating = false;

    void Start ()
    {
        // TODO Obtener el tipo de premio
        prizeType = GameManager.Instance.currPrize;

        CreatePrize();
	}
	
	void Update ()
    {
        if (isRotating)
        {
            transform.Rotate(vRotation * velRotation);
        }
    }

    private void CreatePrize()
    {
        switch (prizeType)
        {
            case PrizeType.Credits:
                {
                    break;
                }
            case PrizeType.Gift_Box:
                {

                    break;
                }
        }
    }

    public void StartAnimation(float velRotation, float velCoin, float timeSpawnCoin)
    {
        StartRotation(velRotation, vRotation);
    }

    public void StopAnimation()
    {
        StopRotation();
    }

    private void StartRotation(float vel, Vector3 vDir)
    {
        vRotation = vDir;
        velRotation = vel;

        isRotating = true;
    }

    private void StopRotation()
    {
        isRotating = false;
    }
}

public enum PrizeType
{
    Gift_Box,
    Credits     // Billete
}