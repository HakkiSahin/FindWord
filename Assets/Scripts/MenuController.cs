using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField] List<GameObject> menuPanels;

    int activePanel=0, lastPanel;

    public void SelectMenu(int selectIndex)
    {
        lastPanel = activePanel;
        activePanel = selectIndex;
        menuPanels[activePanel].SetActive(true);
        menuPanels[lastPanel].SetActive(false);

    }

    public void CloseMenu(GameObject obj)
    {
        obj.SetActive(false);
    }


    public void OpenMenu(GameObject obj)
    {
        obj.SetActive(true);
    }
}
