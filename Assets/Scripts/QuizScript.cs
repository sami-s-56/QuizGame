using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(XMLHandler))]
public class QuizScript : MonoBehaviour
{
    
    [Header("UI Elements")]
    [SerializeField] Text questionText;
    [SerializeField] List<Text> optionTexts = new List<Text>();
    [SerializeField] GameObject quizScreen;
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject finishButton;

    [SerializeField]
    XMLHandler xmlHandler;

    //Score Screen Elements (To be shifted to new script)
    [SerializeField] GameObject scoreScreen;
    [SerializeField] Text statusText;
    [SerializeField] Text scoreText;

    [SerializeField] Text statText;
  
    [Header("Utility Variables")]
    int pos = 0;    //Position which will be randomized
    int attemptedPos = 0;   //Position to navigate through attemptedQuestions list
    
    QuizData currentQuestion;
    
    string selectedAnswer;

    List<QuizData> dataToUse = new List<QuizData>();
    List<QuizData> attemptedQuestions = new List<QuizData>();
    List<string> selectedAnswers = new List<string>();
    
    int playerScore = 0;
    Subject selectedSubject;

    public void StartGame()
    {
        quizScreen.SetActive(true);

        //Initialize XML Handler
        xmlHandler = FindObjectOfType<XMLHandler>();
        if(xmlHandler == null)
        {
            statText.text = "XML Handler not found";
            Invoke("ResetStatusText", 2f);
        }

        //LoadQuizData (Use its overload when subject selection is setup)
        dataToUse = xmlHandler.LoadData(selectedSubject.ToString());

        if (dataToUse.Count == 0)
        {
            statText.text = "No Quiz Data";
            Invoke("ResetStatusText", 2f);
            quizScreen.SetActive(false);
            startScreen.SetActive(true);
        }
        else
        {
            
            print(dataToUse.Count);
            pos = Random.Range(0, dataToUse.Count);
            LoadNewData();
        }
        
    }

    public void SetSubject(Subject s)
    {
        selectedSubject = s;
    }

    //Load Data (Both next and previous)
    void LoadNewData()
    {
        currentQuestion = dataToUse[pos];

        questionText.text = dataToUse[pos].question;
        List<Text> optionTextsRandom = new List<Text>(optionTexts);

        int optionBox = Random.Range(0, optionTextsRandom.Count);
        optionTextsRandom[optionBox].text = dataToUse[pos].correctOption;
        optionTextsRandom.RemoveAt(optionBox);

        optionBox = Random.Range(0, optionTextsRandom.Count);
        optionTextsRandom[optionBox].text = dataToUse[pos].option1;
        optionTextsRandom.RemoveAt(optionBox);

        optionBox = Random.Range(0, optionTextsRandom.Count);
        optionTextsRandom[optionBox].text = dataToUse[pos].option2;
        optionTextsRandom.RemoveAt(optionBox);

        optionBox = Random.Range(0, optionTextsRandom.Count);
        optionTextsRandom[optionBox].text = dataToUse[pos].option3;
        optionTextsRandom.RemoveAt(optionBox);

        attemptedQuestions.Add(dataToUse[pos]);
        dataToUse.RemoveAt(pos);
    }

    private void CheckAnswer()
    {
        //Check for current answer
        if (selectedAnswer == currentQuestion.correctOption)
        {
            playerScore++;
        }
    }

    private void ShowResults()
    {
        quizScreen.SetActive(false);
        scoreScreen.SetActive(true);
        
        float percent = ((float)playerScore / (float)attemptedQuestions.Count) * 100;   //Replace by current list count
        //print(percent);

        if(percent >= 40)
        {
            statusText.text = "Congratulations! You Win";
        }
        else
        {
            statusText.text = "You Lose";
        }

        scoreText.text = "Your Score: " + playerScore + " / " + attemptedQuestions.Count;

    }

    //Next button funtionality
    public void OnNext()
    {
        CheckAnswer();
        ResetToggles();
        selectedAnswers.Add(selectedAnswer);
        
        if(dataToUse.Count > 0)
        {
            pos = Random.Range(0, dataToUse.Count);
            LoadNewData();
        }
        //Change Condition
        if (dataToUse.Count == 0)
        {
            //pos = 0;
            //ShowResults();
            finishButton.SetActive(true);
            nextButton.SetActive(false);
        }
    }

    void ResetToggles()
    {
        GetComponentInChildren<ToggleGroup>().allowSwitchOff = true;
        List<Toggle> toggles = new List<Toggle>(GetComponentsInChildren<Toggle>());
        foreach (var item in toggles)
        {
            item.isOn = false;
        }
    }

    //Previous button funtionality
    public void OnPrev()
    {
        pos--;
        if (pos == -1)
        {
            pos = dataToUse.Count - 1;
        }
        LoadNewData();
    }

    public void OnFinish()
    {
        CheckAnswer();  //Answer of last question
        selectedAnswers.Add(selectedAnswer);    //Maintaining list of answers selected along with attempted questions
        ShowResults();
    }

    //Function to call when an option is selected
    public void OnOptionSelect(int no)
    {
        selectedAnswer = optionTexts[no].text;
        GetComponentInChildren<ToggleGroup>().allowSwitchOff = false;
    }

    void ResetStatusText()
    {
        statText.text = "";
    }
}
