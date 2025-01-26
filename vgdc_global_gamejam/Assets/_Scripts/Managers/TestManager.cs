using UnityEngine;

public class TestManager : MonoBehaviour
{
    private KeyCode TEST_SCORE_ADD_KEY = KeyCode.M;
    private KeyCode TEST_CURSE_RA_KEY = KeyCode.R;
    private KeyCode TEST_DEBUFF_SELECTION_KEY = KeyCode.V;
    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(TEST_SCORE_ADD_KEY))
            {
                TestScoreAdd();
            }
            else if (Input.GetKeyDown(TEST_CURSE_RA_KEY))
            {
                EventMessenger.TriggerEvent(EventKey.FlashCurseOfRa);
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
