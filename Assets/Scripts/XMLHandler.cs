using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class XMLHandler : MonoBehaviour
{
    string path = "/Resources/QuizData.xml";
    List<QuizData> dataList;

    [SerializeField] Text statText;

    private void Awake()
    {
        path = Application.dataPath + path;
    }

    public void CreateOrAdd(List<QuizData> qd)
    {
        if (!File.Exists(path))
        {
            //Create File and add data
            XDocument xDocument = new XDocument(
            new XElement("QuizData",
                from data in qd
                select new XElement("Data", new XAttribute("Subject", data.subject),
                    new XElement("Question", data.question),
                    new XElement("CorrectOption", data.correctOption),
                    new XElement("OptionB", data.option1),
                    new XElement("OptionC", data.option2),
                    new XElement("OptionD", data.option3)
                )
            ));
            xDocument.Save(path);
        }
        else
        {
            XDocument xDocument = XDocument.Load(path);
            if (xDocument == null)
            {
                statText.text = "File Not Loaded...";
                Invoke("ResetStatusText", 2f);
            }
            else
            {
                xDocument.Element("QuizData").Add(
                        from data in qd
                        select new XElement("Data", new XAttribute("Subject", data.subject),
                        new XElement("Question", data.question),
                        new XElement("CorrectOption", data.correctOption),
                        new XElement("OptionB", data.option1),
                        new XElement("OptionC", data.option2),
                        new XElement("OptionD", data.option3)
                    ));
            }
            xDocument.Save(path);
        }
    }

    public List<QuizData> LoadData()
    {
        dataList = new List<QuizData>();

        if (File.Exists(path))
        {
            XDocument xDocument = XDocument.Load(path);
            IEnumerable<XElement> dataSet = xDocument.Descendants("Data");

            foreach (var data in dataSet)
            {
                string question = data.Element("Question").Value;
                string correctOption = data.Element("CorrectOption").Value;
                string optionB = data.Element("OptionB").Value;
                string optionC = data.Element("OptionC").Value;
                string optionD = data.Element("OptionD").Value;

                dataList.Add(new QuizData(question, correctOption, optionB, optionC, optionD));
            }

            
        }
        else
        {
            statText.text = "File Not Found...";
            Invoke("ResetStatusText", 2f);
        }

        return dataList;
    }

    // *Todo once subject seletion is complete
    public List<QuizData> LoadData(string sub)
    {
        if (File.Exists(path))
        {
            dataList = new List<QuizData>();

            XDocument xDocument = XDocument.Load(path);

            //Query for specific subject
            var dataSet = from subjectdata in xDocument.Descendants("Data")
                          where (string)subjectdata.Attribute("Subject") == sub
                          select subjectdata;

            foreach (var data in dataSet)
            {
                string question = data.Element("Question").Value;
                string correctOption = data.Element("CorrectOption").Value;
                string optionB = data.Element("OptionB").Value;
                string optionC = data.Element("OptionC").Value;
                string optionD = data.Element("OptionD").Value;

                dataList.Add(new QuizData(question, correctOption, optionB, optionC, optionD));
            }

            
        }
        else
        {
            statText.text = "File Not Found...";
            Invoke("ResetStatusText", 2f);
            
        }
        return dataList;
    }

    void ResetStatusText()
    {
        statText.text = "";
    }
}
