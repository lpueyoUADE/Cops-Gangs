using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float goToDefeatTime = 5;
    float goToVictoryTime = 5;
    [SerializeField] private RyderModel _ryderModel;
    [SerializeField] private GameObject armorItem;
    [SerializeField] private GameObject lifeItem;
    [SerializeField] private GameObject moneyItem;
    
    
    private DynamicItemRoulette Roulette;
    
    private Dictionary<ItemType, GameObject> items;
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        RyderModel.OnPlayerDeadAction += OnPlayerDeadActionHandler;
        ClownModel.ClownDeadAction += OnClownDeadActionHandler;
        
        items = new Dictionary<ItemType, GameObject>(){
            { ItemType.Shield, armorItem },
            { ItemType.Life, lifeItem },
            { ItemType.Money, moneyItem }};
        
        Roulette = new(
            new()
            {
                { ItemType.Shield },
                { ItemType.Life },
                { ItemType.Money }
            },
            (0, _ryderModel.MaxShieldPoints),
            (0, _ryderModel.MaxLifePoints),
            () => { return _ryderModel.CurrentShieldPoints; },
            () => { return _ryderModel.CurrentLifePoints; }
        );
    }

    public GameObject RollDynamicItem()
    {
        return items[Roulette.RollItem()];
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
