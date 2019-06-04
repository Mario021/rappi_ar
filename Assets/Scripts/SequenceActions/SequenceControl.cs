using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceControl : MonoBehaviour
{
    public ElementSequence[] arrElementAction;

    public int currElementAction = 0;

    public UnityEvent OnStartSequence;
    public UnityEvent OnFinishSequence;

    public void StartSequence()
    {
        OnStartSequence.Invoke();

        currElementAction = 0;

        StartElementAction(currElementAction);
    }

    public void FinishSequence()
    {
        OnFinishSequence.Invoke();
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
        if(currElementAction >= arrElementAction.Length - 1)
        {
            FinishSequence();

            return;
        }

        // Iniciar siguiente secuencia
        StartElementAction(currElementAction);
    }

    public delegate void OnFinishElementAction();
    public static event OnFinishElementAction onFinishAction;
}
