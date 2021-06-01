using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenNavigation : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    GameObject startScreen;
    [SerializeField]
    GameObject adminPanel;
    [SerializeField]
    GameObject authPanel;
    [SerializeField]
    GameObject quizScreen;
    [SerializeField]
    Text subjectText;

    [SerializeField] Text statusText;

    List<string> subjects;
    int pos = 0;

    QuizScript qScript;

    private void Start()
    {
        subjects = new List<string>(System.Enum.GetNames(typeof(Subject)));

        qScript = GetComponent<QuizScript>();
        if(qScript == null)
        {
            statusText.text =  "Quiz Script not found";
            Invoke("ResetStatusText", 2f);
        }

        if(PlayerPrefs.HasKey("Pos")) 
        {
            pos = PlayerPrefs.GetInt("Pos");
        }
        else
        {
            pos = 0;
        }

        subjectText.text = subjects[pos];
    }

    public void OnPlay()
    {
        Subject s = (Subject)System.Enum.Parse((typeof(Subject)),subjectText.text);
        PlayerPrefs.SetInt("Pos", pos);
        print(s);
        qScript.SetSubject(s);
        
        startScreen.SetActive(false);
        qScript.StartGame();
    }

    public void OnAdmin()
    {
        authPanel.SetActive(true);
        adminPanel.GetComponent<Image>().enabled = true;
        startScreen.SetActive(false);
    }

    public void OnNext()
    {
        pos++;
        if(pos >= subjects.Count)
        {
            pos = 0;
        }
        subjectText.text = subjects[pos];
    }

    public void OnPrev()
    {
        pos--;
        if (pos <= -1)
        {
            pos = subjects.Count - 1;
        }
        subjectText.text = subjects[pos];
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    void ResetStatusText()
    {
        statusText.text = "";
    }
}
