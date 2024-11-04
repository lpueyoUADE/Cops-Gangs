using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float goToDefeatTime = 5;
    float goToVictoryTime = 5;

    [Header("Dynamic Item Roullete")]
    [SerializeField] private GameObject shieldItem;
    [SerializeField] private GameObject lifeItem;
    [SerializeField] private GameObject moneyItem;
    
    DynamicItemRoulette dynamicRoulette;
    
    Dictionary<ItemType, GameObject> items;
    
    RyderModel _ryderModel;

    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _ryderModel = FindObjectOfType<RyderModel>();

        RyderModel.OnPlayerDeadAction += OnPlayerDeadActionHandler;
        ClownModel.ClownDeadAction += OnClownDeadActionHandler;
        
        items = new Dictionary<ItemType, GameObject>(){
            { ItemType.Shield, shieldItem },
            { ItemType.Life, lifeItem },
            { ItemType.Money, moneyItem }};

        dynamicRoulette = new(
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

    public void InstatiateRollDynamicItem(Vector3 position)
    {
        var item = items[dynamicRoulette.RollItem()];
        Instantiate(item, position, item.transform.rotation);
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
