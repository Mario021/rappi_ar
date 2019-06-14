using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RappiMainMenu : MonoBehaviour
{
    public GameObject panelListPrize;
    public Button bttStart;

    [Header("Prize Selector")]
    public Image imagePrize;
    public TextMeshProUGUI namePrize;

    [Header("Prize Elements")]
    public GameObject prefUIPrizeElement;
    public Transform ContentPrizes;

    private MusicControl _musicControl;

    void Start ()
    {
        _musicControl = FindObjectOfType<MusicControl>();

        if (GameManager.Instance.currPrize == PrizeType.None)
            bttStart.interactable = false;

        SetInfoSelectorPrize();
    }

    /// <summary>
    /// Llenar listado con los premios disponibles
    /// </summary>
    private void FillPrizeElements()
    {
        // Obtener listado de premios
        DataPrize[] dataPrizes = GameManager.Instance.GetAllDataPrize();

        // Crear listado
        foreach(DataPrize dp in dataPrizes)
        {
            UIPrizeElement uip = Instantiate(prefUIPrizeElement, ContentPrizes).GetComponent<UIPrizeElement>();

            uip.SetInfo(dp);
        }

        dataPrizes = null;
    }

    public void OnSelectPrize(UIPrizeElement prizeElement)
    {
        GameManager.Instance.currPrize = prizeElement.prizeType;
        SetInfoSelectorPrize();

        bttStart.interactable = true;
    }

    public void SetActivePanelListPrize(bool value)
    {
        panelListPrize.SetActive(value);

        if(ContentPrizes.childCount <= 0)
            FillPrizeElements();
    }

    private void SetInfoSelectorPrize()
    {
        if (GameManager.Instance.currPrize == PrizeType.None)
            return;

        //DataPrize dp = GameManager.Instance.GetDataPrize(GameManager.Instance.currPrize);
        
        //imagePrize.overrideSprite = dp.iconPrize;
        //namePrize.text = dp.namePrize;
    }

    public void GoToAR()
    {
        if (GameManager.Instance.currPrize == PrizeType.None)
            return;

        GameManager.Instance.LoadScene(GameManager.Instance.idSceneAR);
    }

    public void SetAudio(bool value)
    {
        _musicControl.setAudio(value);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
