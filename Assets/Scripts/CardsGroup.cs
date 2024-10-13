using UnityEngine;
using UnityEngine.UI;
public class CardsGroup : MonoBehaviour
{
    void Start()
    {
        var rect = GetComponent<RectTransform>();
        var grid = GetComponent<GridLayoutGroup>();
        if(LevelManager.level > 6)
            grid.spacing = new Vector2(50, 50);
        if(LevelManager.level > 9)
            grid.cellSize = new Vector2(150, 150);
        switch (LevelManager.level)
        {
            case 1:
                rect.sizeDelta = new Vector2(700, 670);
                break;
            case 2:
                rect.sizeDelta = new Vector2(900, 670);
                break;
            case 3:
                rect.sizeDelta = new Vector2(1100, 670);
                break;
            case 4:
                rect.sizeDelta = new Vector2(1300, 670);
                break;
            case 5:
                rect.sizeDelta = new Vector2(1100, 770);
                break;
            case 6:
                rect.sizeDelta = new Vector2(1600, 770);
                grid.spacing = new Vector2(50, 100);
                break;
            case 7:
                rect.sizeDelta = new Vector2(900, 1000);
                break;
            case 8:
                rect.sizeDelta = new Vector2(1470, 1000);
                break;
            case 9:
                rect.sizeDelta = new Vector2(1250, 1000);
                break;
            case 10:
                rect.sizeDelta = new Vector2(1290, 1000);
                break;
            //case 11:
            //    rect.sizeDelta = new Vector2(700, 670);
            //    break;
            //case 12:
            //    rect.sizeDelta = new Vector2(700, 670);
            //    break;
            //case 13:
            //    rect.sizeDelta = new Vector2(700, 670);
            //    break;
            //case 14:
            //    rect.sizeDelta = new Vector2(700, 670);
            //    break;
            //case 15:
            //    rect.sizeDelta = new Vector2(700, 670);
            //    break;
            //case 16:
            //    rect.sizeDelta = new Vector2(700, 670);
            //    break;
        }
    }
}