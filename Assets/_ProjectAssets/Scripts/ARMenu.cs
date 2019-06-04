using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARMenu : MonoBehaviour
{
    [Header("Seleccion de modelos")]
    public Transform ContentBttns;
    public Button bttnResetRotation;
    public GameObject prefBttonSelect;

    [Header("Feedback Tracking")]
    public RectTransform imgStateTarget;
    public RectTransform imgSearchTarget;
    public Color colorNotFound;
    public Color colorFound;
    public Color colorSearching;
    public float timeAlphaTransition = .8f;

    public enum StateTracking { FOUND, NOT_FOUND, SEARCHING}

    private void Start()
    {
        
    }

    /// <summary>
    /// Crear los botones para cambiar de modelos. 
    /// </summary>
    /// <param name="modelControl">Objeto que contiene los modelos y su informacion</param>
    public void FillBttnsTracket(ModelARControl modelControl)
    {
        bttnResetRotation.onClick.RemoveAllListeners();
        bttnResetRotation.onClick.AddListener(() => modelControl.ResetRotation());

        foreach (Transform child in ContentBttns)
        {
            Destroy(child.gameObject);
        }

        ModelTarget[] modelsTarget = modelControl.modelsTarget;

        foreach (ModelTarget m in modelsTarget)
        {
            ButtonTarget bttnTarget = Instantiate(prefBttonSelect, ContentBttns).GetComponent<ButtonTarget>();
            bttnTarget.setListenerButton(modelControl, m);
        }
    }

    /// <summary>
    /// Cambiar el valor de interaccion del boton dependiendo de las variables de cada target
    /// </summary>
    /// <param name="modelsTarget">Arreglo de todos los modelos en pantalla</param>
    public void SetInteractableBttnsTracket(ModelTarget[] modelsTarget)
    {
        ButtonTarget[] bttnsTarget = GetComponentsInChildren<ButtonTarget>();

        foreach (ModelTarget m in modelsTarget)
        {
            ButtonTarget bttnTarget = bttnsTarget.First((bTarget) => bTarget.IdTarget == m.Id);

            bool isInteractable = (m.isOverlap) ? true : !m.IsActive;
            bttnTarget.setInteractable(true);
        }
    }

    /// <summary>
    /// Activar/Desactivar interacion de los botones que cambian los modelos 
    /// </summary>
    /// <param name="value"></param>
    public void SetInteractableBttnsTracket(bool value)
    {
        // TODO Agregar referencia transform para obtener los componentes. 
        ButtonTarget[] bttnsTarget = GetComponentsInChildren<ButtonTarget>();

        foreach(ButtonTarget b in bttnsTarget)
        {
            b.setInteractable(value);
        }
    }

    /// <summary>
    /// Activar/Desactivar interaccion del boton de rotacion
    /// </summary>
    /// <param name="value"></param>
    public void SetInteractableBttnRotation(bool value)
    {
        bttnResetRotation.interactable = value;
    }

    /// <summary>
    /// Activar/Desactivar imagen que indica la busqueda de un marcador
    /// </summary>
    /// <param name="value"></param>
    public void SetActiveSearchTarget(bool value)
    {
        imgSearchTarget.gameObject.SetActive(value);

        if (value)
        {
            Image imgSearching = imgSearchTarget.GetComponent<Image>();

            // Modificar alfa de la imagen entre dos valores
            LeanTween.value(gameObject, .2f, 1f, timeAlphaTransition).setOnUpdate((float val) => {
                var tempColor = imgSearching.color;
                tempColor.a = val;
                imgSearching.color = tempColor;
            }).setLoopPingPong();
        }
        else
        {
            LeanTween.cancel(imgSearchTarget);
        }
    }

    /// <summary>
    /// Modificar elementos de la interfaz al cambiar de estado
    /// </summary>
    /// <param name="currState">Estado actual del tracking</param>
    public void SetStateTarget(StateTracking currState)
    {
        switch (currState)
        {
            case StateTracking.FOUND:
                {
                    // Cambiar color de la imagen que indica el estado de tracking del marcador
                    LeanTween.color(imgStateTarget, colorFound, .5f);
                    break;
                }
            case StateTracking.NOT_FOUND:
            case StateTracking.SEARCHING:
                {
                    // Cambiar color de la imagen que indica el estado de tracking del marcador
                    LeanTween.color(imgStateTarget, colorNotFound, .5f);
                    break;
                }
        }
    }
}
