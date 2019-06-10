using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
    // BORRAR
    public TrophiesImageTarget activeTracker;

    private PubDataContainer.PubData currPubData;
    private ARMenu _ARMenu;

    // END BORRAR

    public enum StateTracking
    {
        FOUND,
        LOST,
        SEARCHING
    }

    public StateTracking currStateTracking = StateTracking.SEARCHING;

    private ARRappiMenu _ARRappiMenu;

    private static ARManager _instance;
    public static ARManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

#if UNITY_EDITOR
    public string testPubName;
#endif

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        /*
        currPubData = GeneralManager.GetPubSelected();

#if UNITY_EDITOR
        if(currPubData == null) currPubData = GeneralManager.SelectPub(testPubName);
#endif
        LoadSceneTrackers();
        */
    }

    void Start ()
    {
        _ARRappiMenu = FindObjectOfType<ARRappiMenu>();

        // Cambiar feedback interfaz
        //_ARMenu.SetStateTarget(ARMenu.StateTracking.NOT_FOUND);
        OnTargetLost();
    }
	/*
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GoBack();
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene(GeneralManager.SceneMenuIndex);
    }
    
    void LoadSceneTrackers()
    {
        foreach (GameObject currPrefab in currPubData.trackersPrefab)
        {
            Instantiate(currPrefab);
        }
    }
    */

    public void AddTargetReference()
    {
        //_ARRappiMenu.SetActiveSearchTarget(false);
    }

    public void WaitTarget()
    {
        //_ARRappiMenu.SetActiveSearchTarget(false);
    }

    public void OnTargetLost()
    {
        //_ARRappiMenu.SetActiveSearchTarget(true);
    }

    /// <summary>
    /// Manejador que modifica al momento de indentificar un modelo.
    /// </summary>
    /// <param name="target"></param>
    public void AddTargetReference(TrophiesImageTarget target)
    {
        // Cambiar feedback interfaz
        //_ARMenu.SetStateTarget(ARMenu.StateTracking.FOUND);
        _ARRappiMenu.SetActiveSearchTarget(false);
        //_ARMenu.SetInteractableBttnRotation(true);

        //if (target == activeTracker)
        //{
        //    _ARMenu.SetInteractableBttnsTracket(target.GetComponent<ModelARControl>().modelsTarget);
        //    return;
        //}

        //activeTracker = target;

        // Agregar botones
        //_ARMenu.FillBttnsTracket(target.GetComponent<ModelARControl>());

    }

    /// <summary>
    /// Manejador que modifica al momento de estar al momento de perder el marcador
    /// </summary>
    public void WaitTarget(int i = 0)
    {
        // Cambiar feedback interfaz
        //_ARMenu.SetStateTarget(ARMenu.StateTracking.NOT_FOUND);
        _ARRappiMenu.SetActiveSearchTarget(false);
        //_ARMenu.SetInteractableBttnRotation(true);

    }

    /// <summary>
    /// Manejador que modifica al perder un marcador
    /// </summary>
    /// <param name="target"></param>
    public void OnTargetLost(TrophiesImageTarget target)
    {
        // Desactivar botones
        //_ARMenu.SetInteractableBttnsTracket(false);

        // Cambiar feedback interfaz
        //_ARMenu.SetStateTarget(ARMenu.StateTracking.SEARCHING);
        _ARRappiMenu.SetActiveSearchTarget(true);
        //_ARMenu.SetInteractableBttnRotation(false);
    }

    public void ChangeModelTarget(ModelARControl mARControl)
    {
        //_ARMenu.SetInteractableBttnsTracket(mARControl.modelsTarget);
    }
}
