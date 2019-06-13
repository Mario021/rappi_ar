using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
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
}