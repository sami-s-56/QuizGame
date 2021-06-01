using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminPanelNavigation : MonoBehaviour
{
    [SerializeField] GameObject StartScreen;
    [SerializeField] GameObject AdminMenuScreen;
    [SerializeField] GameObject AddDataScreen;
    


    //Add button of admins main menu
    public void OnDataAddButton()
    {
        AddDataScreen.SetActive(true);
        AdminMenuScreen.SetActive(false);
    }

    public void OnBackButton(GameObject currentSreen)
    {
        StartScreen.SetActive(true);
        currentSreen.SetActive(false);  //can go back from both login screen and admin menu
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void OnBackToAdminButton()
    {
        AdminMenuScreen.SetActive(true);
        AddDataScreen.SetActive(false);
    }
}
