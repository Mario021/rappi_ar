using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InitGame_1 : ElementSequence
{
    public GameObject[] containers;
    public GameObject bag;
    public Transform contentSpline;

    public float initheightContainers = 0f;
    public float maxheightContainers = 2f;
    public float timeMovContainer = .3f;

    public float scaleBag = 1f;
    public float timeScale = .3f;

    private int _maxSequence = 2;
    private int _currSequence = 0;

    private LTSpline _splineBag;

    void Start()
    {
        _splineBag = GetSplineFromTranform(contentSpline);
    }

    public override void StartElementAction(SequenceControl.OnFinishElementAction onFinish = null)
    {
        base.StartElementAction(onFinish);

        _currSequence = -1;

        initNextSequence();
    }

    private void initNextSequence()
    {
        if(_currSequence >= _maxSequence)
        {
            FinishElementAction();

            return;
        }

        _currSequence++;

        switch (_currSequence)
        {
            case 0:
                {
                    // Aparece Mochila rappi
                    LeanTween.scale(bag, Vector3.one * 1.1f * scaleBag, timeScale).setOnComplete(() =>
                    {
                        LeanTween.scale(bag, Vector3.one * scaleBag, .1f);

                        initNextSequence();
                    });
                    break;
                }
            case 1:
                {

                    int count = containers.Length;

                    // Se elevan los contenedores
                    for(int i = 0; i < containers.Length; i++)
                    {
                        LeanTween.move(containers[i], 
                            new Vector3(containers[i].transform.position.x, maxheightContainers, containers[i].transform.position.z), 
                            timeMovContainer).setOnComplete(() =>
                        {
                            count--;

                            if(count <= 0)
                            {
                                initNextSequence();
                            }
                        });
                    }
                    
                    break;
                }
            case 2:
                {
                    //Mochila rappi ingresa en el contenedor central y desaparece 

                    break;
                }
            case 3:
                {
                    //Bajan los contenedores a su posicion inicial.
                    break;
                }
        }      
    }

    public override void FinishElementAction()
    {
        base.FinishElementAction();
    }

    private LTSpline GetSplineFromTranform(Transform ts)
    {
        Vector3[] arrVectors = new Vector3[ts.childCount];

        for (int i = 0; i < ts.childCount; i++)
        {
            arrVectors[i] = ts.GetChild(i).position;
        }

        return new LTSpline(arrVectors);
    }
}
