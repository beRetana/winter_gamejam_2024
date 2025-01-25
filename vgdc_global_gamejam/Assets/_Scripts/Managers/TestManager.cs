using UnityEngine;

public class TestManager : MonoBehaviour
{
    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                TestScoreAdd();
            }
        }
    }
    private void TestScoreAdd()
    {
        DataMessenger.SetInt(IntKey.ScoreToAdd, 100);
        EventMessenger.TriggerEvent(EventKey.AddScore);
    }
}
