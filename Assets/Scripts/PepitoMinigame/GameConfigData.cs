using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataConfig")]
public class GameConfigData : ScriptableObject
{
    // Obtener/Configurar dificultades
    public enum LevelType
    {
        Low,
        Medium,
        Hard
    }

    [System.Serializable]
    public struct level
    {     
        public LevelType levelType;
        public float valueLvl;
    }

    // Niveles disponibles
    public level[] levelsGame;

    // Velocidades de cada movimiento
    public float[] velSingleShuffle;

    /// <summary>
    /// Obtener valor multiplicador de un nivel.
    /// </summary>
    /// <param name="lvlType">Enum del nivel</param>
    /// <returns>Retorna flotante multiplicador del nivel</returns>
    public float GetValueLevel(LevelType lvlType)
    {
        return levelsGame.Single((l) => l.levelType == lvlType).valueLvl;
    }
}
