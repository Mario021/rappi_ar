using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PepitoMinigameControl : MonoBehaviour
{
    // Contenedores que se moveran en el minijuego. 
    public Container[] arrContainers;

    // Movimientos. Indica los objetos relacionados cuando
    // se realiza un movimiento.
    public ConfigTypeMove[] configMoves;

    // Objetos relacionados a un tipo de movimiento
    [System.Serializable]
    public struct ConfigTypeMove
    {
        public Type_Move typeMove;
        public int posObj1;
        public int posObj2;
    }

    // Direccion del movimiento
    public enum Dir_Move
    {
        None,
        Left,
        Right
    }

    // direccion actual
    [SerializeField]
    private Dir_Move _currDirMove;

    /**
     * Tipo de movimiento
     * 
     * Solo se soportan 3 tipos de movimientos.
     * Existen 3 tipos de movimientos:
     * - Move_1: Intercambio entre posicion 1 y posicion 2
     * - Move_2: Intercambio entre posicion 1 y posicion 3
     * - Move_3: Intercambio entre posicion 2 y posicion 3
     */
    public enum Type_Move
    {
        None,
        Move_1,
        Move_2,
        Move_3
    }

    // Tipo movimiento actual
    [SerializeField]
    private Type_Move _currTypeMove;

    // Cantidad maxima de movimientos
    public int maxMoves = 10;

    // Cantidad de movimientos realizados.
    [SerializeField]
    private int _countMoves = 0;

    // Velocidad de movimiento y en que momento cambia

    // Movimiento asociado a diferentes rutas que recorren los
    // contenedores.
    [System.Serializable]
    public class pathContainer
    {
        public string name;
        public Type_Move typeMove;
        public Path[] arrPathMovement;

        [System.Serializable]
        public struct Path
        {
            public string namePath;
            public Transform[] arrPath;
        }
    }

    // Conjunto de caminos para los diferentes tipos de movimientos
    public pathContainer[] splinePaths;

    public bool isShuffling = false;

    public UnityEvent OnInitShuffle;
    public UnityEvent OnFinishShuffle;

    public UnityEvent OnInitGame;
    public UnityEvent OnFinishGame;

    void OnEnable()
    {
        // create the path
        //cr = new LTSpline(new Vector3[] { trans[0].position, trans[1].position, trans[2].position, trans[3].position, trans[4].position });
        // cr = new LTSpline( new Vector3[] {new Vector3(-1f,0f,0f), new Vector3(0f,0f,0f), new Vector3(4f,0f,0f), new Vector3(20f,0f,0f), new Vector3(30f,0f,0f)} );
    }

    void Start ()
    {
        Init();
	}
	
	void Update ()
    {
        
	}

    /// <summary>
    /// Inicializar minijuego
    /// </summary>
    public void Init()
    {
        _countMoves = 0;
        _currDirMove = Dir_Move.None;
        _currTypeMove = Type_Move.None;
        isShuffling = true;

        OnInitGame.Invoke();
    }


    /// <summary>
    /// Revolver contenedores
    /// </summary>
    public void Shuffle()
    {
        if (_countMoves >= maxMoves)
        {
            return;
        }       

        // Obtener direccion movimiento
        int dirMove = UnityEngine.Random.Range(1, Enum.GetNames(typeof(Dir_Move)).Length);

        _currDirMove = (Dir_Move)dirMove;

        // Obtener tipo de movimiento
        int typeMove = (int)_currTypeMove;
        
        // Evitar repetir el movimiento anterior
        while(typeMove == (int)_currTypeMove)
        {
            typeMove = UnityEngine.Random.Range(1, Enum.GetNames(typeof(Type_Move)).Length);
        }
       
        _currTypeMove = (Type_Move)typeMove;
        Debug.Log(_currDirMove);

        // Revolver contenedores
        //Move_1: Intercambio entre posicion 1 y posicion 2
        //Move_2: Intercambio entre posicion 1 y posicion 3
        //Move_3: Intercambio entre posicion 2 y posicion 3
        MoveContainer(_currTypeMove, _currDirMove);

        // TODO : Esperar a que se termine de ejecutar el movimiento

        _countMoves++;

        if(_countMoves >= maxMoves)
        {
            OnFinishGame.Invoke();
        }

        //Debug.Log("move: " +  (Type_Move)typeMove);
    }

    private void MoveContainer(Type_Move tMove, Dir_Move dMove)
    {
        Container temp1 = null, temp2 = null;

        ConfigTypeMove move = configMoves.Single((m) => m.typeMove == tMove);
        pathContainer pathsContainer = splinePaths.Single((s) => s.typeMove == tMove);

        temp1 = arrContainers[move.posObj1];
        temp2 = arrContainers[move.posObj2];

        pathContainer.Path path_1;
        pathContainer.Path path_2;

        // Obtener path de acuerdo a la direccion
        path_1 = pathsContainer.arrPathMovement.Single((p) => p.namePath == ((dMove == Dir_Move.Right) ? "Up" : "Down"));
        path_2 = pathsContainer.arrPathMovement.Single((p) => p.namePath == ((dMove == Dir_Move.Right) ? "Down" : "Up"));

        // Obtener el spline que recorrera el contenedor
        // El segundo container siempre tendra el path inverso
        LTSpline spline_1 = GetSplineFromTranform(path_1.arrPath, false);
        LTSpline spline_2 = GetSplineFromTranform(path_2.arrPath, true);

        // Mover contenedores
        arrContainers[move.posObj1].Move(spline_1, .5f);
        arrContainers[move.posObj2].Move(spline_2, .5f);

        arrContainers[move.posObj1] = temp2;
        arrContainers[move.posObj2] = temp1;

        spline_1 = null;
        spline_2 = null;
        pathsContainer = null;
        temp1 = null;
        temp2 = null;
    }

    /// <summary>
    /// Obtener arreglo de vectores a partir de un arreglo de Transform
    /// </summary>
    /// <param name="ts">arreglo de Transform</param>
    /// <returns>Arreglo de Vector3</returns>
    private Vector3[] GetVector3fromTransform(Transform[] ts)
    {
        Vector3[] arrVectors = new Vector3[ts.Length + 2];

        for(int i = 0; i < ts.Length; i++)
        {
            arrVectors[i+1] = ts[i].position;
        }

        // En Leantween la primera ultima posicion se utilizan como referencia y 
        // no para definir la ruta.
        arrVectors[0] = ts[0].position;
        arrVectors[arrVectors.Length - 1] = ts[ts.Length - 1].position;

        return arrVectors;
    }

    /// <summary>
    /// Obtener un Spline (LeanTween) a partir de un arreglo de Transforms
    /// </summary>
    /// <param name="ts">Arreglo de Transform</param>
    /// <param name="isReverse">Indica si la direccion del spline. Si es "false" entonces se invierte el arreglo de Transform</param>
    /// <returns>Retorna un LTSpline</returns>
    private LTSpline GetSplineFromTranform(Transform[] ts, bool isReverse)
    {
        Transform[] arrT = new Transform[ts.Length];

        Array.Copy(ts, arrT, ts.Length);

        if (isReverse)
            Array.Reverse(arrT);

        return new LTSpline(GetVector3fromTransform(arrT));
    }

    void OnDrawGizmos()
    {
        //// Debug.Log("drwaing");
        //if (cr == null)
        //    OnEnable();
        //Gizmos.color = Color.red;
        //if (cr != null)
        //    cr.gizmoDraw(); // To Visualize the path, use this method
    }
}
