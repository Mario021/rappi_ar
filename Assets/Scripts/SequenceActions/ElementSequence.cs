using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementSequence : MonoBehaviour
{
    private SequenceControl.OnFinishElementAction _onFinishActionElement = null;

    public UnityEvent OnStart;
    public UnityEvent OnFinish;

    public virtual void StartElementAction(SequenceControl.OnFinishElementAction onFinish = null)
    {
        OnStart.Invoke();

        if (onFinish != null)
        {
            _onFinishActionElement = onFinish;
        }
    }

    public virtual void FinishElementAction()
    {
        OnFinish.Invoke();

        if (_onFinishActionElement != null)
        {
            _onFinishActionElement();
        }

        _onFinishActionElement = null;
    }
}
