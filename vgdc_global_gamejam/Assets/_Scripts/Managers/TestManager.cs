using UnityEngine;

public class TestManager : MonoBehaviour
{
    private KeyCode TEST_SCORE_ADD_KEY = KeyCode.M;
    private KeyCode TEST_BOARD_DEBUFF_KEY = KeyCode.N;
    private KeyCode TEST_SHOT_DEBUFF_KEY = KeyCode.B;
    private KeyCode TEST_DEBUFF_SELECTION_KEY = KeyCode.V;
    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(TEST_SCORE_ADD_KEY))
            {
                TestScoreAdd();
            }
            else if (Input.GetKeyDown(TEST_BOARD_DEBUFF_KEY))
            {
                EventMessenger.TriggerEvent(EventKey.ApplyBoardDebuff);
            }
            else if (Input.GetKeyDown(TEST_SHOT_DEBUFF_KEY))
            {
                EventMessenger.TriggerEvent(EventKey.ApplyShotDebuff);
            }
            else if (Input.GetKeyDown(TEST_DEBUFF_SELECTION_KEY))
            {
                EventMessenger.TriggerEvent(EventKey.ShowDebuffSelection);
            }
        }
    }
    private void TestScoreAdd()
    {
        DataMessenger.SetInt(IntKey.ScoreToAdd, 100);
        EventMessenger.TriggerEvent(EventKey.AddScore);
    }
}
