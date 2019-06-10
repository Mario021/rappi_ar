using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARRappiMenu : MonoBehaviour
{
    [Header("Waiting Initialize")]
    // Interfaz iniciar partida
    public WindowMovement startGameContent;

    [Header("Selection Container")]
    // interfaz de seleccion de contenedor
    public WindowMovement selectorContainer;
    public TextMeshProUGUI textMessage;

    [Header("Amaze")]
    // Interfaz "asombrar" al escoger la opcion correcta

    [Header("Message Feedback")]
    // Interfaz mensaje de feedback
    public WindowMovement feedbackMessage;

    [Header("UI Sound")]
    public Button bttnSound;

    [Header("Feedback Tracking")]
    public RectTransform imgSearchTarget;
    public float timeAlphaTransition = .8f;

    void Start ()
    {
		
	}

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    /// <summary>
    /// Mostrar/Ocultar interfaz para iniciar juego
    /// </summary>
    public void SetActiveWaitInitGame(bool value)
    {
        startGameContent.setActiveWindow(value);
    }

    /// <summary>
    /// Mostrar/Ocultar interfaz de seleccion contenedor
    /// </summary>
    public void SetActiveSelectorContainer(bool value)
    {
        selectorContainer.setActiveWindow(value);
    }

    public void SetActiveMessage(bool value)
    {
        if (value)
        {
            feedbackMessage.setActiveWindow(true);
            textMessage.text = GameManager.Instance.GetMessageFeedbackGameOver();
        }
        else
        {
            feedbackMessage.setActiveWindow(false);
            textMessage.text = "";
        }
    }

    /// <summary>
    /// Activar/Desactivar celebracion
    /// </summary>
    /// <param name="value"></param>
    public void SetActiveCelebration(bool value)
    {
        Debug.Log("Celebracion!: " + value);
    }

    /// <summary>
    /// Mostrar interfaz de asombro
    /// </summary>
    /// <param name="time"> Tiempo que se muestra el efecto</param>
    public void ActiveAmaze(float time)
    {
        Debug.Log("Asombrado!");
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
}