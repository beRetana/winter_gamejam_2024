using UnityEngine;

public class TestManager : MonoBehaviour
{
    private KeyCode TEST_SCORE_ADD_KEY = KeyCode.M;
    private KeyCode TEST_BOARD_DEBUFF_KEY = KeyCode.N;
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
        }
    }
    private void TestScoreAdd()
    {
        DataMessenger.SetInt(IntKey.ScoreToAdd, 100);
        EventMessenger.TriggerEvent(EventKey.AddScore);
    }
}
