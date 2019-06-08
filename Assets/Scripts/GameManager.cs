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

    public enum GameState
    {
        None,
        Searching_Target,   // Buscando marcador
        Waiting_Initialize, // Esperando iniciar juego
        Starting,           // Empezando (secuencia)
        Playing,            // Jugando
        Paused,             // Al perder el marcador
        Finishing,          // Finalizando (secuencia)
        Game_Over           // Juego terminado
    }

    [Header("Game")]
    private GameState _currState = GameState.None;
    private GameState _lastState = GameState.None;
    public GameState CurrState
    {
        get
        {
            return _currState;
        }
        set
        {
            _lastState = _currState;

            _currState = value;

            OnStateChange();
        }
    }

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

    public void InitGame()
    {
        CurrState = GameState.Searching_Target;
    }

    public void StartGame()
    {
        CurrState = GameState.Starting;
    }

    /// <summary>
    /// Cambiar estado del juego
    /// </summary>
    /// <param name="state">Index de GameState</param>
    public void ChangeState(int state)
    {
        ChangeState((GameState)state);
    }

    public void ChangeState(GameState state)
    {
        CurrState = state;
    }

    private void OnStateChange()
    {
        switch (_currState)
        {
            case GameState.Searching_Target:
                {
                    
                    break;
                }
            case GameState.Waiting_Initialize:
                {
                    // Mostrar interfaz:
                    //  + Boton "iniciar juego"
                    break;
                }
            case GameState.Starting:
                {
                    // Iniciar secuencia "Esconder mochila"
                    break;
                }
            case GameState.Playing:
                {
                    // Iniciar juego "Revolver contenedores"
                    PepitoMinigameControl.Instance.InitGame();
                    break;
                }
            case GameState.Finishing:
                {
                    // Indicar si ganó o perdió a traves de las secuencias
                    // + Iniciar secuencia "Levantar contenedor escogido"
                    //  ++ Correcto: Iniciar secuencia "Mostrar premio"
                    //  ++ Incorrecto: Iniciar secuencia "Incorrecto"
                    break;
                }
            case GameState.Game_Over:
                {
                    // Indica que el juego ha sido finalizado

                    // Reiniciar escena
                    break;
                }
            case GameState.Paused:
                {
                    // Detener juego o secuencia
                    break;
                }
        }
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
