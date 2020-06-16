using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class LevelPanelScript : MonoBehaviour
{
    /// <summary>
    /// Кнопки на панели
    /// </summary>
    private List<GameObject> buttons = new List<GameObject>();

    /// <summary>
    /// Шаблон кнопки
    /// </summary>
    private GameObject button;

    private void Awake()
    {
        button = LevelsMenuManager.Instance.button;

        int count = LevelsMenuManager.Instance.CountLevelsPanels;
        for (int i = 0; i < count; i++)
        {
            GameObject button = Instantiate(this.button, gameObject.transform);
            buttons.Add(button);
        }
    }

    public void InitButtons(int numPanel)
    {
        if (numPanel <= 0 || numPanel > LevelsMenuManager.Instance.GetMaxCountPanels()) return; 

        // макс уровень на панели
        int maxLevel = numPanel * LevelsMenuManager.Instance.CountLevelsPanels;
        int diff = 0;
        if (maxLevel > LevelsMenuManager.Instance.GetMaxCountLevels())
        {
            diff = maxLevel - LevelsMenuManager.Instance.GetMaxCountLevels();
            maxLevel = LevelsMenuManager.Instance.GetMaxCountLevels();
        }

        // необх кол-во кнопок на панели
        int countButtons = LevelsMenuManager.Instance.CountLevelsPanels - diff;
        // кол-во активных кнопок
        int countActiveButtons = buttons.Count(rec => rec.gameObject.activeInHierarchy == true);

        // активируем или деактивируем кнопки по условию
        if (countButtons > countActiveButtons)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (!buttons[i].activeSelf)
                {
                    buttons[i].SetActive(true);
                    countActiveButtons++;
                }

                if (countButtons == countActiveButtons) break;
            }
        }
        else if (countButtons < countActiveButtons)
        {
            for (int i = buttons.Count - 1; i >= 0; i--)
            {
                if (buttons[i].activeSelf)
                {
                    buttons[i].SetActive(false);
                    countActiveButtons--;
                }

                if (countButtons == countActiveButtons) break;
            }
        }

        // расставляем уровни по активным кнопкам
        for (int i = buttons.Count - 1; i >= 0; i--)
        {
            if (buttons[i].activeInHierarchy)
            {
                buttons[i].GetComponent<LevelButtonScript>().SetLevel(maxLevel--);
            }
        }
    }
}
