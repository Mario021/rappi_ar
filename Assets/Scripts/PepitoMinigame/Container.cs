﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Container : MonoBehaviour
{
    public int initPos = 0;

    public GameObject currPrize;

    public bool IsSelected = false;
    public bool HasPrize = false;
    public bool IsMoving = false;

    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;

	void Start ()
    {
        HasPrize = (currPrize != null) ;
        OnDeselectedContainer();
    }

    public void Move(LTSpline spline, float vel, PepitoMinigameControl.OnFinishShuffleCallback onFinish = null)
    {
        IsMoving = true;

        LeanTween.move(gameObject, spline, vel).setOnComplete(() =>
        {
            IsMoving = false;

            if (onFinish != null)
                onFinish();
        }
        );
    }

    /// <summary>
    /// Obtener index inicial del objeto en el juego.
    /// </summary>
    /// <returns>Retonar entero que indica la posicion inicial</returns>
    public int GetInitPos()
    {
        return initPos;
    }

    public void OnSelectContainer()
    {
        OnSelected.Invoke();

        IsSelected = true;

        // Activar interfaz (flecha)
    }

    public void OnDeselectedContainer()
    {
        OnDeselected.Invoke();

        IsSelected = false;

        // Desactivar interfaz (flecha)
    }

    public Transform GetPositionPrize()
    {
        return transform.GetChild(0);
    }

    public GameObject CreatePrize(Transform parent = null)
    {
        GameObject prize = null;

        prize = Instantiate(currPrize, parent);
        prize.transform.localScale = Vector3.zero;
        prize.transform.position = parent.position;
        prize.name = "Prize";
        prize.transform.rotation = parent.localRotation;

        return prize;
    }
}