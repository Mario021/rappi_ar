﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_EndGame_Correct : ElementSequence
{
    [Header("Elements")]
    public GameObject[] containers;
    public GameObject bag;
    public Material matBag;

    [Header("General")]
    public float[] timeToNextAction;

    [Header("Action 1")]
    // Accion 1: Aparece mochila Rappi
    public float scaleBag = 1f;
    public float timeShowBag = .3f;

    [Header("Action 2")]
    // Accion 2: Se elevan los contenedores
    public float maxheightContainers = 2f;
    public float timeMovContainer = .3f;

    [Header("Action 3")]
    // Accion 3: Mochila se posiciona al centro
    public float timetoPosBag;
    // Posicion final de la mochila en la que se muestra
    // el premio.
    public Transform pointPosBag;

    [Header("Action 4")]
    // Accion 4: Desaparecer contenedores y abrir mochila
    public float timeHideContainer = .5f;
    //public float timeOpenBag = .4f;

    [Header("Action 5")]
    // Accion 5: Aparece premio girando desde el interior de la mochila
    public float timeShowPrize = .5f;
    public Transform posPrize;
    public Transform posBagPrize;

    //----------------------------------------
    private Container _correctContainer = null;
    private Animator _animBag = null;

    private int _maxSequence = 5;
    private int _currSequence = 0;

    void Start()
    {
        _animBag = bag.GetComponent<Animator>();
    }

    public override void StartElementAction(SequenceControl.OnFinishElementAction onFinish = null)
    {
        base.StartElementAction(onFinish);

        Debug.Log("Inicio Secuencia 2");

        _animBag.SetFloat("direction", -1f);
        _animBag.Play("openningBag", 0, 0f);
        matBag.SetFloat("_Transparency", 0f);
        bag.transform.localScale = Vector3.zero;

        _currSequence = -1;

        for (int i = 0; i < containers.Length; i++)
        {
            Container c = containers[i].GetComponent<Container>();

            if (c.HasPrize)
            {
                _correctContainer = c;
                break;
            }
        }        

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
                    // Aparece mochila Rappi

                    // TODO aparecer en la posicion del premio

                    // Obtener posicion en donde aparece la mochila. La posicion se encuentra en el contenedor 
                    // correcto.
                    Transform initPoint = _correctContainer.GetPositionPrize();

                    bag.transform.position = initPoint.position;
   
                    LeanTween.move(bag,
                            new Vector3(bag.transform.position.x, pointPosBag.position.y, bag.transform.position.z),
                            timeMovContainer).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
                            {
                                LeanTween.delayedCall(timeToNextAction[_currSequence], () => { initNextSequence(); });
                            });

                    LeanTween.scale(bag, Vector3.one * 1.1f * scaleBag, timeShowBag/2).setOnComplete(() =>
                    {
                        LeanTween.scale(bag, Vector3.one * scaleBag, .1f);                      
                    });

                    break;
                }
            case 1:
                {

                    int count = containers.Length;

                    // Se elevan los contenedores
                    for (int i = 0; i < containers.Length; i++)
                    {
                        LeanTween.move(containers[i],
                            new Vector3(containers[i].transform.position.x, maxheightContainers, containers[i].transform.position.z),
                            timeMovContainer).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
                            {
                                count--;

                                if (count <= 0)
                                {
                                    LeanTween.delayedCall(timeToNextAction[_currSequence], () => { initNextSequence(); });
                                }
                            });
                    }

                    break;
                }
            case 2:
                {
                    // Mochila se posiciona al centro                
 
                    LeanTween.move(bag, pointPosBag.position, timetoPosBag).setEase(LeanTweenType.easeInSine).setOnComplete(() =>
                    {
                        LeanTween.delayedCall(timeToNextAction[_currSequence], () => { initNextSequence(); });
                    });

                    break;
                }
            case 3:
                {
                    // Desaparecer contenedores y abrir mochila

                    int count = containers.Length;
                   
                    // Achicar contenedores
                    for (int i = 0; i < containers.Length; i++)
                    {
                        LeanTween.scale(containers[i], Vector3.zero, timeHideContainer).setEase(LeanTweenType.easeOutSine);
                    }

                    // Abrir mochila
                    _animBag.SetFloat("direction", 1f);
                    _animBag.Play("openningBag", 0, 0);

                    // Obtener largo de la animacion
                    AnimatorClipInfo[]  m_CurrentClipInfo = _animBag.GetCurrentAnimatorClipInfo(0);
                    float lenghtAnim = m_CurrentClipInfo[0].clip.length;

                    // Terminar accion
                    LeanTween.delayedCall(timeToNextAction[_currSequence] + lenghtAnim, () => { initNextSequence(); });

                    break;
                }
            case 4:
                {
                    // Aparece premio girando desde el interior de la mochila

                    // premio
                    GameObject p = _correctContainer.GetPrize();
                    GameObject.Destroy(p.GetComponent<BoxCollider>());
                    p.transform.localScale = Vector3.zero;
                    p.transform.position = posBagPrize.position;
                    p.name = "Prize";
                    p.transform.SetParent(posBagPrize);

                    LeanTween.move(p,
                            posPrize.position,
                            timeShowPrize).setEase(LeanTweenType.easeOutSine).setOnComplete(() =>
                            {
                                LeanTween.delayedCall(timeToNextAction[_currSequence], () => { FinishElementAction(); });
                            });

                    LeanTween.scale(p, Vector3.one, timeShowPrize / 1.5f);

                    break;
                }
        }
    }

    public override void FinishElementAction()
    {
        base.FinishElementAction();

        Debug.Log("Final Secuencia 2");
    }
}