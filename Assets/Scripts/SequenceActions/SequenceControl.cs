﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceControl : MonoBehaviour
{
    // Indica a que estado del juego pertenece
    public GameState gameStateSequence;

    public ElementSequence[] arrElementAction;

    public int currElementAction = 0;

    private OnFinishSequenceCallback _onFinishSequence = null;

    public UnityEvent OnStartSequence;
    public UnityEvent OnFinishSequence;

    public void StartSequence(OnFinishSequenceCallback onFinish = null)
    {
        OnStartSequence.Invoke();

        _onFinishSequence = onFinish;

        currElementAction = 0;

        StartElementAction(currElementAction);
    }

    public void FinishSequence()
    {
        OnFinishSequence.Invoke();

        if(_onFinishSequence != null)
        {
            _onFinishSequence();
        }
    }

    /// <summary>
    /// Iniciar accion del elemento.
    /// </summary>
    /// <param name="posElement">posicion del elemento</param>
    private void StartElementAction(int posElement)
    {
        currElementAction = posElement;

        arrElementAction[posElement].StartElementAction(() =>
        {
            currElementAction++;

            OnFinishedAction();
        });
    }

    /// <summary>
    /// Iniciar siguiente accion o finalizar secuencia.
    /// </summary>
    private void OnFinishedAction()
    {
        // Finalizar secuencia si se han ejecutado todas las acciones.
        if(currElementAction >= arrElementAction.Length)
        {
            FinishSequence();

            return;
        }

        // Iniciar siguiente secuencia
        StartElementAction(currElementAction);
    }

    public delegate void OnFinishElementActionCallback();
    public static event OnFinishElementActionCallback onFinishAction;

    public delegate void OnFinishSequenceCallback();
    public static event OnFinishSequenceCallback onFinishSequence;
}
