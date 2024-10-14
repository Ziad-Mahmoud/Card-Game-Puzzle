using System;
using UnityEngine;
using UnityEngine.UI;
public class CardsGroup : MonoBehaviour
{
    [Serializable]
    public struct Level
    {
        public Vector2 cardSize;
        public Vector2 spacing;
    }

    [SerializeField] Level[] levels;
    void Start()
    {
        var rect = GetComponent<RectTransform>();
        var grid = GetComponent<GridLayoutGroup>();
        int index = levels.Length - 1;
        if (LevelManager.level < 12)
            index = 0;
        else if (LevelManager.level < 25)
            index = 1;
        else if (LevelManager.level < 49)
            index = 2;
        var level = levels[index];
        grid.cellSize = level.cardSize;
        grid.spacing = level.spacing;
    }
}