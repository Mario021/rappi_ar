using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Data Configuration Game")]
    public GameConfigData configData;

    [Header("Prize Selected")]
    // Premio actual seleccionado
    public PrizeType currPrize = PrizeType.Gift_Box;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }

	void Start ()
    {
		
	}

    /// <summary>
    /// Obtener Informacion de los modelos a cargar (nombre modelo, nombre archivo) de acuerdo
    /// al tipo de premio.
    /// </summary>
    /// <param name="prizeType"></param>
    /// <returns></returns>
    public DataPrize GetDataPrize(PrizeType prizeType)
    {
        return configData.GetPrizeData(prizeType);
    }

    public float[] GetVelocityShuffleGame()
    {
        return configData.GetVelocityShuffle();
    }
}
