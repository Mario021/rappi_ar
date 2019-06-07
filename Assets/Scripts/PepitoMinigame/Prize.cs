using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    public const string pathResources = "Prizes/";

    public PrizeType prizeType;

    public Transform posCoin;
    private GameObject _coin;
    public Material matCoin;

    private Vector3 vRotation = Vector3.up;
    private float velRotation = 0;
    public bool isRotating = false;
    private Transform currPrizeRotate = null;

    void Start ()
    {
        prizeType = GameManager.Instance.currPrize;

        CreatePrize();
    }
	
	void Update ()
    {
        if (isRotating && currPrizeRotate != null)
        {
            currPrizeRotate.Rotate(vRotation * velRotation);
        }
    }

    private void CreatePrize()
    {
        // Obtener nombre de los modelos a cargar
        DataPrize data = GameManager.Instance.GetDataPrize(prizeType);

        matCoin.SetFloat("_Transparency", 1f);

        foreach (string fileName in data.nameFile)
        {
            // Cargar modelos desde resources
            GameObject p = Instantiate(Resources.Load<GameObject>(pathResources + fileName));
            p.name = data.namePrize;         

            if (fileName != "Coin")
            {
                currPrizeRotate = p.transform;
                p.transform.SetParent(this.transform);
            }
            else
            {
                p.transform.SetParent(posCoin);
                _coin = p;
            }

            p.transform.localPosition = Vector3.zero;
            p.transform.localScale = Vector3.one;
        }
    }

    public void StartAnimation(float velRotation, float velCoin, float timeSpawnCoin)
    {
        StartRotation(velRotation, vRotation);

        // Animacion moneda
        if(posCoin.childCount > 0)
        {
            StartAnimCoin(velCoin, timeSpawnCoin);           
        }
    }

    private void StartAnimCoin(float velCoin, float timeSpawnCoin)
    {
        matCoin.SetFloat("_Transparency", 1f);
        _coin.transform.localPosition = Vector3.zero;

        Vector3 firstPoint = new Vector3(_coin.transform.position.x, _coin.transform.position.y + 3f, _coin.transform.position.z);
        Vector3 lastPoint = new Vector3(firstPoint.x, firstPoint.y - 1f, firstPoint.z);

        // Escalar y fade al aparecer moneda
        LeanTween.scale(_coin, Vector3.one, velCoin/3).
                    setOnUpdate((float f) =>
                    {
                        matCoin.SetFloat("_Transparency", 1 - f);
                    });

        // Movimiento hacia arriba
        LeanTween.move(_coin, firstPoint, velCoin).setOnComplete(() =>
        {
            // Espera para el siguiente movimiento
            LeanTween.delayedCall(.3f, () => 
            {
                // Movimiento hacia abajo
                LeanTween.move(_coin, lastPoint, velCoin / 3).setOnComplete(() =>
                {
                    matCoin.SetFloat("_Transparency", 1f);
                    LeanTween.delayedCall(timeSpawnCoin, () => { StartAnimCoin(velCoin, timeSpawnCoin); });
                }).setOnUpdate((float f) =>
                {
                    matCoin.SetFloat("_Transparency", f);
                }); ;
            });          
        });
    }

    public void StopAnimation()
    {
        StopRotation();

        // Animacion moneda
        if (posCoin.childCount > 0)
        {
            LeanTween.cancelAll();
        }
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