using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataConfig")]
public class GameConfigData : ScriptableObject
{
    // Obtener/Configurar dificultades

    [System.Serializable]
    public struct level
    {     
        public LevelType levelType;
        public float valueLvl;
    }

    [Header("Niveles del juego")]
    // Niveles disponibles
    public level[] levelsGame;

    [Header("Velocidades de los movimientos")]
    // Velocidades de cada movimiento
    public float[] velSingleShuffle;

    [Header("references Prize Resources")]
    public DataPrize[] dataPrize;

    /// <summary>
    /// Obtener valor multiplicador de un nivel.
    /// </summary>
    /// <param name="lvlType">Enum del nivel</param>
    /// <returns>Retorna flotante multiplicador del nivel</returns>
    public float GetValueLevel(LevelType lvlType)
    {
        return levelsGame.Single((l) => l.levelType == lvlType).valueLvl;
    }

    public DataPrize GetPrizeData(PrizeType prize)
    {
        return dataPrize.Single((p) => p.prizeType == prize);
    }

    public float[] GetVelocityShuffle()
    {
        return velSingleShuffle;
    }
}

[System.Serializable]
public struct DataPrize
{
    public string namePrize;
    public string[] nameFile;
    public PrizeType prizeType;
}

public enum LevelType
{
    Low,
    Medium,
    Hard
}