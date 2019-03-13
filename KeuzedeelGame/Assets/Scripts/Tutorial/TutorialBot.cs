using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class TutorialBot : MonoBehaviour
{
    [SerializeField]
    private List<MoveablePositions> m_MoveablePositionsCollections;
    [SerializeField]
    private TextMeshProUGUI m_BotText;

    private MoveablePositions m_MoveablePositions;

    private int m_Step;

    private void Start()
    {
        AskPlayerForTutorial();
    }

    private void AskPlayerForTutorial()
    {
        //Show 3 buttons(Calibratie tutorial, Level select tutorial, no tutorial)
    }

    private void ActivateLevelSelectTutorial()
    {
        m_Step = 0;
        ++m_Step;
        ShowLevelSelect();
    }

    private void ActivateCalibrationTutorial()
    {
        m_Step = 0;
        ++m_Step;
        ShowCalibration();
    }

    private void ActivateLevelTutorial()
    {

    }

    #region Main Menu Tutorial

    public void ShowCalibration()
    {
        m_MoveablePositions = m_MoveablePositionsCollections[0];

       switch (m_Step)
        {
            case 1: // Shows calibration machine
                ShowText("This machine will set all the heights of the walls to your height" +
                    Environment.NewLine + "Press the button console to let it check your height");
                ++m_Step;
                break;
            case 2: // Tells it checks height
                ShowText("This is your height"
                    + Environment.NewLine + "When you this is ok press the console again");
                ++m_Step;
                break;
            case 3: // Tells it checks duck height
                ShowText("This is your duck height duck as far as you are ok"
                    + Environment.NewLine + "press the console again");
                ++m_Step;
                break;
            case 4: // Tells second step
                ShowText("Now thats done do you want do a other tutorial?");
                ++m_Step;
                break;
            case 5: // Tells you are done
                m_Step = 0;
                AskPlayerForTutorial();
                break;
        }
    }

    private void ShowLevelSelect()
    {
        m_MoveablePositions = m_MoveablePositionsCollections[1];

        switch (m_Step)
        {
            case 1: // Shows level select
                ShowText("Here you can select the different difficulties");
                ++m_Step;
                break;
            case 2: // Tells about level select
                ShowText("When you select one the levels will show up here");
                ++m_Step;
                break;
            case 3: // Goes to levels when you press a level select button
                ShowText("Do you want to do a other tutorial?");
                AskPlayerForTutorial();
                m_Step = 0;
                break;
        }
    }

    #endregion

    private void LevelTutorial()
    {

    }

    private void GoBackToIdle()
    {

    }

    private void ShowText(string text)
    {
        m_BotText.text = text;
    }

    private void ClearCurrentText()
    {

    }

    #region States

    private enum LevelTutorialState
    {
        StartTutorial = 0,
        FirstWallStart,
        FirstWallStop,
        FirstWallEnd,
        SecondWallStart,
        SecondWallStop,
        SecondWallEnd,
        LastWallStart,
        LastWallStop,
        LastWallSlowmotion,
        EndTutorial
    }

    private enum MainMenuTutorialState
    {
        Idle = 0,
        ShowCalibration,
        ShowLevelSelect
    }

    #endregion

    private IEnumerator ShowTextRoutine()
    {
        //Cuts up string
        //Builds up string one letter at a time
        //Gives that to show text
        yield return 0;
    }
}

[Serializable]
public class MoveablePositions
{
    private int m_PositionsIndex;
    [SerializeField]
    private List<Transform> m_Positions;
}

public enum TutorialArea
{
    MainMenu = 0,
    Level
}
