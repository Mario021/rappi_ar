using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InitGame_1 : ElementSequence
{
    [Header("Elements")]
    public GameObject[] containers;
    public GameObject bag;
    public Material matBag;

    [Header("Action 1")]
    public float scaleBag = 1f;
    public float timeScale = .3f;

    [Header("Action 2")]
    public float maxheightContainers = 2f;
    public float timeMovContainer = .3f;
    public Transform contentSpline;

    [Header("Action 3")]
    public float timeHideBag = .5f;

    [Header("Action 4")]
    public float initheightContainers = 0f;

    private int _maxSequence = 4;
    private int _currSequence = 0;

    private LTSpline _splineBag;

    void Start()
    {
        _splineBag = GetSplineFromTranform(contentSpline);
    }

    public override void StartElementAction(SequenceControl.OnFinishElementAction onFinish = null)
    {
        base.StartElementAction(onFinish);

        Debug.Log("Inicio Secuencia 1");

        matBag.SetFloat("_Transparency", 0f);
        bag.transform.localScale = Vector3.zero;

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
                        LeanTween.delayedCall(.3f, () => { initNextSequence(); });
                        
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
                            timeMovContainer).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
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
                    LeanTween.move(bag, _splineBag, timeHideBag).setEase(LeanTweenType.easeInSine).setOnComplete(() => 
                    {
                        LeanTween.delayedCall(.3f, () => { initNextSequence(); });
                    });

                    LeanTween.scale(bag, Vector3.zero, .5f).
                        setOnUpdate((float f) => 
                        {
                            matBag.SetFloat("_Transparency", f);
                        }).
                        setDelay(timeHideBag/3);
                    break;
                }
            case 3:
                {
                    int count = containers.Length;

                    //Bajan los contenedores a su posicion inicial.
                    for (int i = 0; i < containers.Length; i++)
                    {
                        LeanTween.move(containers[i],
                            new Vector3(containers[i].transform.position.x, initheightContainers, containers[i].transform.position.z),
                            timeMovContainer).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
                            {
                                count--;

                                if (count <= 0)
                                {
                                    FinishElementAction();
                                }
                            });
                    }
                    break;
                }
        }      
    }

    public override void FinishElementAction()
    {
        base.FinishElementAction();

        Debug.Log("Final Secuencia 1");
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

    //private void setAlpha(float alpha, object obj)
    //{
    //    Debug.Log("alpha: " + alpha);
    //    Debug.Log("object: " + obj);
    //    GameObject go = (GameObject)obj;
    //    Material mat = go.GetComponent<Material>();
    //    //Color c = mat.color;

    //    //c.a = alpha;

    //    //mat.color = c;
    //}
}
