using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class FightPanel : MonoBehaviour
{   
    [Header("Referenes")]
    [SerializeField] private GraphicRaycaster _raycaster;
    [SerializeField] private GameObject _fightPanel;
    [SerializeField] private GameObject _randomDicePanel;
    [SerializeField] private HorizontalLayoutGroup _playerLayourtGroup;
    [SerializeField] private TextMeshProUGUI _playerVictoryPoints;
    [SerializeField] private TextMeshProUGUI _enemyVictoryPoints;
    [SerializeField] private Transform _enemyCubePos;
    [SerializeField] private Transform _playerCubePos;
    [SerializeField] private Transform _playerDicesParent;
    [SerializeField] private Transform _enemyDicesParent;
    [SerializeField] private List<Dice> _playerDices;
    [SerializeField] private List<Dice> _enemyDices;
    [SerializeField] private Dice _randomDice;
    [SerializeField] private TextMeshProUGUI _randomDiceText1;
    [SerializeField] private TextMeshProUGUI _randomDiceText2;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Transform _playerAbilitiesPanel;
    [SerializeField] private PlayerAbilities _playerAbilities;

    private List<Dice> _enemyActiveDices = new List<Dice>();
    private List<Dice> _enemyBestDices = new List<Dice>();
    private List<Dice> _playerActiveDices = new List<Dice>();
    private List<AbilityCard> _playerCards = new List<AbilityCard>();

    public static System.Action<int> OnFightEnd;

    private int _playerScore = 0;
    private int _enemyScore = 0;
    private int _reward = 0;

    private bool _readyToPlace = true;

    //[SerializeField] private AbilityCard[] _playerAbilities;
    [SerializeField] private DiceHolder _playerDice;
    [SerializeField] private DiceHolder _enemyDice;

    private MapEntity _minion;
    private Marker _marker;

    public static FightPanel Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        MapEntity.OnTargetArrived += StartFight;
        _playerDice.OnDicePlace += OnDicePlacement;
        Dice.OnDiceRoll += OnDiceRoll;
    }

    private void OnDisable()
    {
        MapEntity.OnTargetArrived -= StartFight;
        _playerDice.OnDicePlace -= OnDicePlacement;
        Dice.OnDiceRoll -= OnDiceRoll;
    }

    public void StartFight(MapEntity entity, Marker marker)
    {
        if (marker.StarCount == 0)
            return;

        _marker = marker;
        _minion = entity;

        InitFight();
        InitPlayerDices();
        InitEnemyDices();
        ApplyEnemyAbilities();
        ApplyPlayerAbilities();

        StartCoroutine(AfterStartFight());
    }

    private IEnumerator AfterStartFight()
    {
        yield return new WaitForEndOfFrame();
        _playerLayourtGroup.enabled = false;
        //foreach(var a in _playerAbilities)
        //    a.Init();
    }

    private void ApplyMarkerBonus()
    {
        switch(_marker.BonusLoot)
        {
            case Marker.MarkerBonus.bonusFightDice:
                ResourceHolder.Instance.BonusFightDices++;
                _resultText.text += "\nParmanent: +1 Combat Dice";
                break;
            default:
                break;
        }
    }

    private void InitFight()
    {
        _enemyCubePos.gameObject.SetActive(true);
        _playerCubePos.gameObject.SetActive(true);
        _fightPanel.SetActive(true);
        _resultText.text = "";
        Time.timeScale = 0f;
        _playerLayourtGroup.enabled = true;
        _reward = _marker.StarCount;
        _readyToPlace = true;
        _randomDice.Disable(false);
        _playerScore = 0;
        _enemyScore = 0;
        _enemyBestDices.Clear();
        _playerVictoryPoints.text = "" + _playerScore;
        _enemyVictoryPoints.text = "" + _enemyScore;
    }

    private void InitPlayerDices()
    {
        _playerActiveDices.Clear();
        for (int i = 0; i < _playerDices.Count; i++)
        {
            _playerDices[i].Disable();
            _playerDices[i].gameObject.SetActive(false);
            if (i < _minion.DiceCount * 2 + ResourceHolder.Instance.BonusFightDices)
            {
                _playerDices[i].gameObject.SetActive(true);
                _playerActiveDices.Add(_playerDices[i]);
            }
        }
    }

    private void InitEnemyDices()
    {
        _enemyActiveDices.Clear();
        for (int i = 0; i < _enemyDices.Count; i++)
        {
            _enemyDices[i].gameObject.SetActive(false);
            if (i < _marker.StarCount * 2)
            {
                _enemyDices[i].gameObject.SetActive(true);
                _enemyDices[i].Roll();
                _enemyActiveDices.Add(_enemyDices[i]);
            }
        }
        SortEnemyDicesPull();
    }

    private void SortEnemyDicesPull()
    {
        _enemyBestDices.Clear();
        foreach (var d in _enemyActiveDices)
        {
            _enemyBestDices.Add(d);
        }
        
        if (_enemyBestDices.Count > _playerActiveDices.Count)
        {
            _enemyBestDices.Sort(SortDices);
            _enemyBestDices.RemoveRange(_playerActiveDices.Count, (_enemyBestDices.Count - _playerActiveDices.Count));
        }
    }

    private void ApplyPlayerAbilities()
    {
        if(_playerAbilities._playerAbilities != null)
        {
            foreach(AbilityID id in _playerAbilities._playerAbilities)
            {
                Ability ability = _playerAbilities.GetAbility(id);
                if(ability != null)
                {
                    AbilityCard card = Instantiate(ability.Card, _playerAbilitiesPanel.position, Quaternion.identity, _playerAbilitiesPanel);
                    _playerCards.Add(card);
                    card.Init();
                }
            }
        }
    }

    private void ApplyEnemyAbilities()
    {
        switch(_marker.Ability)
        {
            case Marker.MarkerAbility.reroll1s:
                _infoText.text = "Fight effect:\nenemy reroll 1's dices";
                StartCoroutine(EnemyReroll1sDices(0.75f));
                break;
            case Marker.MarkerAbility.none:
            default:
            _infoText.text = "";
                break;
        }
    }

    private IEnumerator EnemyReroll1sDices(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        foreach (Dice dice in _enemyActiveDices)
        {
            
            if (dice.Value == 1)
            {
                dice.Value = -1;
                dice.RollWithAnim();
            }
        }
        SortEnemyDicesPull();
    }

    public void RollAllDices()
    {
        StartCoroutine(RollDiceWithDelay());
    }

    private IEnumerator RollDiceWithDelay()
    {
        for (int i = 0; i < _playerActiveDices.Count; i++)
        {
            if(_playerActiveDices[i].Value != -1)
                continue;
            _playerActiveDices[i].RollWithAnim();
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private int SortDices(Dice d1, Dice d2)
    {
        if(d1.Value < d2.Value)
        {
            return 1;
        }
        else if (d1.Value > d2.Value)
        {
            return -1;
        }
        return 0;
    }

    public void EndFight()
    {
        _enemyCubePos.gameObject.SetActive(false);
        _playerCubePos.gameObject.SetActive(false);
        if (_playerScore < _enemyScore)
        {
            _reward = 0;
            _resultText.text = "Lose";
        }
        else
        {
            _resultText.text = $"Victory:\nCandles + {_reward}";
            ApplyMarkerBonus();
        }
        OnFightEnd?.Invoke(_reward);
        if(_reward > 0)
        {
            _marker.Disable();
            _marker.StarCount = 0;
        }
        _minion.Candles += _reward;
        _minion.DiceCount = _playerActiveDices.Count / 2;
        if(_minion.DiceCount == 0)
        {
            _minion.BackHome();
        }
        _randomDicePanel.SetActive(false);
        StartCoroutine(ClosePanelWithDelay(2f));
    }

    private IEnumerator ClosePanelWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        for(int i = 0; i <_playerCards.Count; i++)
        {
            Destroy(_playerCards[i].gameObject);
        }
        _playerCards.Clear();
        _fightPanel.SetActive(false);
    }

    private void CompareDices()
    {
        if(_playerDice.ContainedDice.Value > _enemyDice.ContainedDice.Value)
        {
            _playerScore++;
        }
        else if(_playerDice.ContainedDice.Value < _enemyDice.ContainedDice.Value)
        {
            _enemyScore++;
        }
        _playerVictoryPoints.text = "" + _playerScore;
        _enemyVictoryPoints.text = "" + _enemyScore;
    }

    private void OnDiceRoll(Dice dice)
    {
        if (_randomDicePanel.activeSelf)
        {
            if (dice.Value <= 3)
            {
                _enemyScore++;
                _randomDiceText1.text = "Lose";
                _randomDiceText2.gameObject.SetActive(false);
            }
            else
            {
                _playerScore++;
                _randomDiceText1.gameObject.SetActive(false);
                _randomDiceText2.text = "Victory";
            }
            StartCoroutine(LateEndFight());
        }
    }

    private void OnDicePlacement(DiceHolder holder, AbilityCard card)
    {
        if (_readyToPlace == false || card != null)
            return;
        
        EnemySelectDice();
        _playerActiveDices.Remove(_playerDice.ContainedDice);
        CompareDices();

        _readyToPlace = false;
        StartCoroutine(HideDices());
    }

    private IEnumerator LateEndFight()
    {
        yield return new WaitForSecondsRealtime(3f);
        EndFight();
    }

    private void EnemySelectDice()
    {
        Dice enemyDice = _enemyBestDices[Random.Range(0, _enemyBestDices.Count)];
        _enemyDice.SelectDice(enemyDice);
        _enemyBestDices.Remove(_enemyDice.ContainedDice);
        enemyDice.SetImageByValue();
    }

    private void EnableRandomVictory()
    {
        _randomDiceText1.text = "1-3: Lose";
        _randomDiceText2.text = "4-5: Victory";
        _randomDiceText1.gameObject.SetActive(true);
        _randomDiceText2.gameObject.SetActive(true);

        _randomDicePanel.SetActive(true);
    }

    private IEnumerator HideDices()
    {
        yield return new WaitForSecondsRealtime(1f);
        _playerDice.DeselectDice();
        _enemyDice.DeselectDice();
        _readyToPlace = true;

        if (_enemyBestDices.Count == 0 || _playerActiveDices.Count == 0)
        {
            if(_enemyScore == _playerScore)
            {
                EnableRandomVictory();
            }
            else EndFight();
        }
    }

    public List<Dice> GetEnemyDice() => _enemyBestDices;
    public List<Dice> GetPlayerDice() => _playerActiveDices;
}
