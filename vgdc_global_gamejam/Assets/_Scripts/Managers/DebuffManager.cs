using System;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    private const float DEBUFF_PEG_CHANCE = 0.6f;
    private const float DEBUFF_BOARD_CHANCE = 0.8f;
    private const float DEBUFF_SHOT_CHANCE = 1f;

    [SerializeField] private GameObject _debuffSelectionPrefab;

    private GameObject _debuffSelectionObject;

    // Button ID : DebuffType, SpecificDebuffType (as an int)
    private Dictionary<int, (DebuffType, int)> _debuffTypes;

    private const int DEBUFF_CHOICE_COUNT = 3;

    public enum DebuffType
    {
        Peg, Board, Shot
    }

    private void OnEnable()
    {
        //EventMessenger.StartListening(EventKey.ApplyDebuff, ApplyDebuff);
        EventMessenger.StartListening(EventKey.ShowDebuffSelection, ShowDebuffSelection);
        EventMessenger.StartListening(EventKey.DebuffSelected, DebuffSelected);
    }

    private void OnDisable()
    {
        //EventMessenger.StopListening(EventKey.ApplyDebuff, ApplyDebuff);
        EventMessenger.StopListening(EventKey.ShowDebuffSelection, ShowDebuffSelection);
        EventMessenger.StopListening(EventKey.DebuffSelected, DebuffSelected);
    }

    // Displays the selection of curses to choose from
    private void ShowDebuffSelection()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, false);

        _debuffTypes = new();

        _debuffSelectionObject = Instantiate(_debuffSelectionPrefab);
        
        for (int i = 0; i < DEBUFF_CHOICE_COUNT; ++i)
        {
            // (DebuffType, int-casted SpecificDebuffType, string representation of SpecificDebuffType)
            var info = GetRandomDebuff();
            while (_debuffTypes.ContainsValue((info.Item1, info.Item2)))
            {
                info = GetRandomDebuff();
            }
            _debuffTypes.Add(i, (info.Item1, info.Item2));

            _debuffSelectionObject.transform.GetChild(0).GetChild(i).name = i + " " + _debuffTypes[i].Item1 + 
                " " + info.Item3;
        }
    }

    // Destroys the selection menu for curses and updates the value fo the curse selected
    private void DebuffSelected()
    {
        int debuffSelection = DataMessenger.GetInt(IntKey.DebuffSelection);

        EventKey eventKey;

        switch (_debuffTypes[debuffSelection].Item1)
        {
            case DebuffType.Peg:
                // Trigger the event for applying a peg debuff (Peg Manager)
                eventKey = EventKey.ApplyPegDebuff;
                break;
            case DebuffType.Board:
                // Trigger the event for Tilting the board (Board Manager)
                eventKey = EventKey.ApplyBoardDebuff;
                break;
            case DebuffType.Shot:
                // Trigger the event for changing the score multiplier (Shot Manager)
                eventKey = EventKey.ApplyShotDebuff;
                break;
            default:
                // Unreachable
                eventKey = EventKey.ApplyPegDebuff;
                break;
        }

        DataMessenger.SetInt(IntKey.DebuffEnumID, _debuffTypes[debuffSelection].Item2);
        // Trigger the event
        EventMessenger.TriggerEvent(eventKey);

        DataMessenger.SetBool(BoolKey.IsGameActive, true);

        Destroy(_debuffSelectionObject);
    }

    // Returns a random debuff category (peg, shot, game), its specific type and name
    private (DebuffType, int, string) GetRandomDebuff()
    {
        float rand = UnityEngine.Random.Range(0.0f, 1.0f);
        DebuffType debuffType = DebuffType.Peg;

        if (rand < DEBUFF_PEG_CHANCE)
        {
            debuffType = DebuffType.Shot;
        }
        else if (rand < DEBUFF_BOARD_CHANCE)
        {
            debuffType = DebuffType.Board;
        }
        else // if (rand < DEBUFF_SHOT_CHANCE)
        {
            debuffType = DebuffType.Shot;
        }

        int specificDebuffType;
        string debuffTypeName;

        switch (debuffType)
        {
            default:
            case DebuffType.Peg:
                {
                    PegManager.PegDebuffType pegDebuffType =
                        (PegManager.PegDebuffType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PegManager.PegDebuffType)).Length);
                    specificDebuffType = (int)pegDebuffType;
                    debuffTypeName = pegDebuffType.ToString();
                    break;
                }
            case DebuffType.Board:
                {
                    BoardManager.BoardDebuffType boardDebuffType =
                        (BoardManager.BoardDebuffType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(BoardManager.BoardDebuffType)).Length);
                    specificDebuffType = (int)boardDebuffType;
                    debuffTypeName = boardDebuffType.ToString();
                    break;
                }
            case DebuffType.Shot:
                {
                    ShotManager.ShotDebuffType shotDebuffType =
                        (ShotManager.ShotDebuffType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ShotManager.ShotDebuffType)).Length);
                    specificDebuffType = (int)shotDebuffType;
                    debuffTypeName = shotDebuffType.ToString();
                    break;
                }
        }
        return (debuffType, specificDebuffType, debuffTypeName);
    }
    
    // Chooses the Category of debuff to apply
    // What is the difference between this and the GetRandomDebuff() method?
    /*private void ApplyDebuff()
    {
        // Choose top-level debuff type
        float rand = UnityEngine.Random.Range(0.0f, 1.0f);
        DebuffType debuffType = DebuffType.Peg;

        if (rand > DEBUFF_SHOT_CHANCE)
        {
            debuffType = DebuffType.Shot;
        }
        else if (rand > DEBUFF_BOARD_CHANCE)
        {
            debuffType = DebuffType.Board;
        }

        switch (debuffType)
        {
            case DebuffType.Peg:
                EventMessenger.TriggerEvent(EventKey.ApplyPegDebuff);
                break;
            case DebuffType.Board:
                EventMessenger.TriggerEvent(EventKey.ApplyBoardDebuff);
                break;
            case DebuffType.Shot:
                break;
        }
    }*/
}
