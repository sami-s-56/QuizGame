using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminHandler : MonoBehaviour
{
    [Header("Utilities")]
    //XML Stuff
    XMLHandler xmlHandler;
    
    //Quiz Data Variables
    string ques, coa, ob, oc, od;
    Subject sub;
    [SerializeField]
    List<QuizData> dataToAdd;
    List<string> subjects;

    const string password = "M249SAWLMG";

    [Header("UI Elements")]
    [SerializeField] InputField questionText;
    [SerializeField] InputField correctText;
    [SerializeField] InputField opt1Text;
    [SerializeField] InputField opt2Text;
    [SerializeField] InputField opt3Text;
    [SerializeField] Dropdown subjectSelect;
    [SerializeField] Text subjectSelectLabel;

    [SerializeField] InputField passwordText;

    [SerializeField] Text statText;

    [Header("ScreensToLoad")]
    [SerializeField] GameObject AdminMenuScreen;
    [SerializeField] GameObject AuthenticationScreen;

    private void Start()
    {
        xmlHandler = FindObjectOfType<XMLHandler>();
        if(xmlHandler == null)
        {
            statText.text = "XML Handler not found";
            Invoke("ResetStatusText", 2f);
        }
        dataToAdd = new List<QuizData>();

        subjects = new List<string>(System.Enum.GetNames(typeof(Subject)));

        subjectSelect.AddOptions(subjects);
        
    }

    public void OnLoginButton()
    {
        if(passwordText.text == password)
        {
            AdminMenuScreen.SetActive(true);
            AuthenticationScreen.SetActive(false);
        }
        else
        {
            statText.text = "Authentication Failed! Try again...";
            Invoke("ResetStatusText", 2f);
        }
    }

    //Add button of Data adding screen
    public void OnAddButton()
    {
        if(questionText.text == "" || correctText.text == "" || opt1Text.text == "" || opt2Text.text == "" || opt3Text.text == "")
        {
            statText.text = "Fill all feilds";
            Invoke("ResetStatusText", 2f);
        }
        else
        {
            ques = questionText.text;
            coa = correctText.text;
            ob = opt1Text.text;
            oc = opt2Text.text;
            od = opt3Text.text;
            
            sub = (Subject)System.Enum.Parse((typeof(Subject)), subjectSelectLabel.text);
            
            dataToAdd.Add(new QuizData(ques, coa, ob, oc, od, sub));
            ClearText();
        }
    }

    private void ClearText()
    {
        questionText.text = null;
        correctText.text = null;
        opt1Text.text = null;
        opt2Text.text = null;
        opt3Text.text = null;
    }

    public void OnSaveButton()
    {
        if(dataToAdd.Count == 0)
        {
            statText.text = "Theres no data to add";
            Invoke("ResetStatusText", 2f);
        }
        else
        {
            xmlHandler.CreateOrAdd(dataToAdd);
        }
    }

    void ResetStatusText()
    {
        statText.text = "";
    }

}
