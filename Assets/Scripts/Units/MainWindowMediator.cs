using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class MainWindowMediator : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _countMoneyText;
        [SerializeField]
        private TMP_Text _countHealthText;
        [SerializeField]
        private TMP_Text _countPowerText;
        [SerializeField]
        private TMP_Text _countPowerEnemyText;
        [SerializeField]
        private Button _addMoneyButton;
        [SerializeField]
        private Button _minusMoneyButton;
        [SerializeField]
        private Button _addHealthButton;
        [SerializeField]
        private Button _minusHealthButton;
        [SerializeField]
        private Button _addPowerButton;
        [SerializeField]
        private Button _minusPowerButton;
        [SerializeField]
        private Button _fightButton;
        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        
        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        
        private Enemy _enemy;
        private void Start()
        {
            _enemy = new Enemy("Enemy StoneEgg");

            _money = CreateDataPlayer(_enemy, DataType.Money);
            _heath = CreateDataPlayer(_enemy, DataType.Health);
            _power = CreateDataPlayer(_enemy, DataType.Power);
            
            Subscribe();
        }

        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _minusMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _minusHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _minusPowerButton.onClick.AddListener(DecreasePower);

            _fightButton.onClick.AddListener(Fight);
        }

        private void OnDestroy()
        {
            Unsubscribe();

            RemoveDataPlayer(ref _money);
            RemoveDataPlayer(ref _heath);
            RemoveDataPlayer(ref _power);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _minusMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _addHealthButton.onClick.RemoveListener(IncreaseHealth);
            _minusHealthButton.onClick.RemoveListener(DecreaseHealth);

            _addPowerButton.onClick.RemoveListener(IncreasePower);
            _minusPowerButton.onClick.RemoveListener(DecreasePower);

            _fightButton.onClick.RemoveListener(Fight);
        }

        private PlayerData CreateDataPlayer(IEnemy enemy, DataType dataType)
        {
            var dataPlayer = new PlayerData(dataType);
            dataPlayer.Attach(enemy);

            return dataPlayer;
        }

        private void RemoveDataPlayer(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }
        private void IncreaseMoney() => AddMoney(1);
        private void DecreaseMoney() => AddMoney(-1);
        private void AddMoney(int addition) => 
            AddToValue(ref _allCountMoneyPlayer, addition, DataType.Money);

        private void IncreaseHealth() => AddHealth(1);
        private void DecreaseHealth() => AddHealth(-1);
        private void AddHealth(int addition) => 
            AddToValue(ref _allCountHealthPlayer, addition, DataType.Health);
        

        private void IncreasePower() => AddPower(1);
        private void DecreasePower() => AddPower(-1);
        private void AddPower(int addition) => 
            AddToValue(ref _allCountPowerPlayer, addition, DataType.Power);
        
        private void AddToValue(ref int value, int addition, DataType dataType)
        {
            value += addition;
            ChangeDataWindow(value, dataType);
        }
       
        private void Fight()
        {
            Debug.Log(_allCountPowerPlayer >= _enemy.CalcPower()
                ? "<color=#07FF00>Win!!!</color>"
                : "<color=#FF0000>Lose!!!</color>");
        }
        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Money:
                    _countMoneyText.text = $"Player Money {countChangeData.ToString()}";
                    _money.Value = countChangeData;
                    break;
                case DataType.Health:
                    _countHealthText.text = $"Player Health {countChangeData.ToString()}";
                    _heath.Value = countChangeData;
                    break;
                case DataType.Power:
                    _countPowerText.text = $"Player Power {countChangeData.ToString()}";
                    _power.Value = countChangeData;
                    break;
            }
            _countPowerEnemyText.text = $"Enemy Power {_enemy.CalcPower()}";
        }
    }
}
