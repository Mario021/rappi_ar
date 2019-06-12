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
        GameManager.Instance.InitGame();
    }

    public void AddTargetReference()
    {
        GameManager.Instance.PauseGame(false);
    }

    public void OnTargetLost()
    {
        GameManager.Instance.PauseGame(true);
    }

    #region ELIMINAR CODIGO
    /// <summary>
    /// Manejador que modifica al momento de indentificar un modelo.
    /// </summary>
    /// <param name="target"></param>
    public void AddTargetReference(TrophiesImageTarget target)
    {

    }

    /// <summary>
    /// Manejador que modifica al momento de estar al momento de perder el marcador
    /// </summary>
    public void WaitTarget()
    {

    }

    /// <summary>
    /// Manejador que modifica al perder un marcador
    /// </summary>
    /// <param name="target"></param>
    public void OnTargetLost(TrophiesImageTarget target)
    {

    }

    public void ChangeModelTarget(ModelARControl mARControl)
    {

    }
    #endregion
}