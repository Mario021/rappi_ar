﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelector : MonoBehaviour
{
    // Indican el nivel. La primera posicion indica menor nivel. Ultima posicion indica mayor nivel
    public Image[] lvlIndicators;
    public Color colorLevel = Color.yellow;

	void Start ()
    {
        ChangeColorIndicators((int)GameManager.Instance.currDifficulty);
    }
	
    /// <summary>
    /// Cambiar nivel del juego. El nivel siempre va en aumento.
    /// Al llegar al nivel maximo se reinicia.
    /// </summary>
    public void SetLevel()
    {
        int maxLevel = LevelType.GetNames(typeof(LevelType)).Length;

        int nextLevel = (int)GameManager.Instance.currDifficulty + 1;

        if ( nextLevel >= maxLevel)
        {
            nextLevel = 0;
        }

        GameManager.Instance.currDifficulty = (LevelType)nextLevel;

        ChangeColorIndicators(nextLevel);
    }

    private void ChangeColorIndicators(int lvl)
    {
        for(int i = 0; i < lvlIndicators.Length; i++)
        {
            lvlIndicators[i].color = (i <= lvl) ? colorLevel : Color.white;
        }
    }
}