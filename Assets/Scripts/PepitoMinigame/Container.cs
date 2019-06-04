using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

	void Start ()
    {
		
	}
	
    public void Move(LTSpline spline, float vel)
    {
        // TODO : Retornar callback al finalizar el movimiento

        LeanTween.move(gameObject, spline, vel);
    }
}
