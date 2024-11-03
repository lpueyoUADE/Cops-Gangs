using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float goToDefeatTime = 5;
    float goToVictoryTime = 5;

    private void Awake()
    {
        RyderModel.OnPlayerDeadAction += OnPlayerDeadActionHandler;
        ClownModel.ClownDeadAction += OnClownDeadActionHandler;
    }

    private void OnDestroy()
    {
        RyderModel.OnPlayerDeadAction -= OnPlayerDeadActionHandler;
        ClownModel.ClownDeadAction -= OnClownDeadActionHandler;
    }

    IEnumerator VictoryOrDefeatCoroutine(string newSceneName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(newSceneName);
    }

    private void OnPlayerDeadActionHandler()
    {
        StartCoroutine(VictoryOrDefeatCoroutine("Defeat", goToDefeatTime));
    }

    private void OnClownDeadActionHandler()
    {
        StartCoroutine(VictoryOrDefeatCoroutine("Victory", goToVictoryTime));
    }
}
