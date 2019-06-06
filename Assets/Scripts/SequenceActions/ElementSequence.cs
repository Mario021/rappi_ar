using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ElementSequence : MonoBehaviour
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

    protected LTSpline GetSplineFromTranform(Transform ts)
    {
        Vector3[] arrVectors = new Vector3[ts.childCount];

        for (int i = 0; i < ts.childCount; i++)
        {
            arrVectors[i] = ts.GetChild(i).position;
        }

        return new LTSpline(arrVectors);
    }
}
