using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Data Configuration Game")]
    public GameConfigData configData;

    [Header("Options Selected")]
    // Premio actual seleccionado
    public PrizeType currPrize = PrizeType.Gift_Box;
    public LevelType currDifficulty = LevelType.Medium;

    [Header("Game")]
    // Indica si el jugador ha ganado la partida
    public bool IsPlayerWinner = false;

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

    private SequenceControl[] _sequenceControls = null;

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

    public void StartTestGame()
    {
        /*Test*/
        _sequenceControls = FindObjectsOfType<SequenceControl>();

        StartGame();
        /*End Test*/
    }

    public void InitGame()
    {
        // Buscar todas las secuencias en la escena
        _sequenceControls = FindObjectsOfType<SequenceControl>();

        CurrState = GameState.Searching_Target;
    }

    public void StartGame()
    {
        CurrState = GameState.Starting;
    }

    /// <summary>
    /// Juego Finalizado
    /// </summary>
    public void FinishGame()
    {
        CurrState = GameState.Finishing;
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
                    // Interfaz buscando marcador
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
                    SequenceControl currSeq = _sequenceControls.SingleOrDefault((s) => s.gameStateSequence == CurrState);

                    if(currSeq == null)
                    {
                        CurrState = GameState.Playing;
                        return;
                    }

                    currSeq.StartSequence(() =>
                    {
                        // Secuencia finalizada
                        CurrState = GameState.Playing;
                    });

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
                    IsPlayerWinner = PepitoMinigameControl.Instance.IsWin;
                    // Indicar si ganó o perdió a traves de las secuencias
                    // + Iniciar secuencia "Levantar contenedor escogido"
                    //  ++ Correcto: 
                    //      --  Iniciar secuencia "Mostrar premio"
                    //      --  Interfaz al finalizar secuencia
                    //  ++ Incorrecto: 
                    //      --  Iniciar secuencia "Incorrecto"
                    //      --  Interfaz al finalizar secuencia

                    SequenceControl currSeq = _sequenceControls.SingleOrDefault((s) => s.gameStateSequence == CurrState);

                    if (currSeq == null)
                    {
                        CurrState = GameState.Game_Over;
                        return;
                    }

                    currSeq.StartSequence(() =>
                    {
                        // Secuencia finalizada
                        CurrState = GameState.Game_Over;
                    });
                    break;
                }
            case GameState.Game_Over:
                {
                    // Indica que el juego ha sido finalizado
                    Debug.Log("Game Over");
                    _sequenceControls = null;
                    CurrState = GameState.None;
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

    public float GetValueLevelGame()
    {
        return configData.GetValueLevel(currDifficulty);
    }
}

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