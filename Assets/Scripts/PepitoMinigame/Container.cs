using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public int initPos = 0;

    public bool IsMoving = false;

	void Start ()
    {
		
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
}
