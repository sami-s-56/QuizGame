using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class QuizData
{
    public string question;
   
    public string correctOption;
    
    public string option1;
    
    public string option2;
   
    public string option3;

    public Subject subject;

    public QuizData()
    {
        
    }

    public QuizData(string q, string coa, string ob, string oc, string od)
    {
        question = q;
        correctOption = coa;
        option1 = ob;
        option2 = oc;
        option3 = od;
    }

    public QuizData(string q, string coa, string ob, string oc, string od, Subject sub)
    {
        question = q;
        correctOption = coa;
        option1 = ob;
        option2 = oc;
        option3 = od;
        subject = sub;
    }
}

public enum Subject
{
    Subject1, Subject2, Subject3, Subject4
};
    

