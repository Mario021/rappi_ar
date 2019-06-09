﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Lift_Container_Selection : ElementSequence
{
    [Header("General")]
    public float[] timeToNextAction;

    [Header("Action")]
    // Accion 1: Aparece Mochila rappi
    public float maxheightContainer = 2f;
    public float timeMovContainer = .3f;

    //----------------------------------------  

    private int _maxSequence = 1;
    private int _currSequence = 0;

    void Start()
    {

    }

    public override void StartElementAction(SequenceControl.OnFinishElementActionCallback onFinish = null)
    {
        base.StartElementAction(onFinish);

        _currSequence = -1;

        initNextSequence();
    }

    private void initNextSequence()
    {
        if (_currSequence >= _maxSequence)
        {
            FinishElementAction();

            return;
        }

        _currSequence++;

        switch (_currSequence)
        {
            case 0:
                {
                    GameObject container = PepitoMinigameControl.Instance.GetContainerSelected();

                    // Se elevar contenedor
                    LeanTween.move(container,
                            new Vector3(container.transform.position.x, maxheightContainer, container.transform.position.z),
                            timeMovContainer).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
                            {
                                LeanTween.delayedCall(timeToNextAction[_currSequence], () => { FinishElementAction(); });
                            });

                    break;
                }
        }
    }

    public override void FinishElementAction()
    {
        base.FinishElementAction();

        Debug.Log("Final Secuencia");
    }
}
