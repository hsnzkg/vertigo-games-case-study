using EventBus.Runtime;
using Project.Scripts.EventBus.Events.GamePlay;
using Project.Scripts.UI.Core;
using UnityEngine;

namespace Project.Scripts.Managers
{
    public static class CurrencyManager
    {
        private const string k_currencyKey = "UserCurrency";
        private static readonly Observable<int> s_currency;

        static CurrencyManager()
        {
            s_currency = new Observable<int>(PlayerPrefs.GetInt(k_currencyKey, 0));
            s_currency.OnChanged += OnCurrencyChanged;
            LoadCurrency();
        }

        private static void OnCurrencyChanged(int obj)
        {
            EventBus<EMoneyProgress>.Raise(new EMoneyProgress(obj));
        }

        private static void LoadCurrency()
        {
            s_currency.Value = PlayerPrefs.GetInt(k_currencyKey, 0);
        }

        public static bool HasCurrency(int amount)
        {
            return s_currency.Value >= amount;
        }

        public static void AddCurrency(int amount)
        {
            s_currency.Value += amount;
            SaveCurrency();
        }
        
        public static void SetCurrency(int amount)
        {
            s_currency.Value = amount;
            SaveCurrency();
        }

        public static bool SubtractCurrency(int amount)
        {
            if (!HasCurrency(amount)) return false;

            s_currency.Value -= amount;
            SaveCurrency();
            return true;
        }

        private static void SaveCurrency()
        {
            PlayerPrefs.SetInt(k_currencyKey, s_currency.Value);
            PlayerPrefs.Save();
        }

        public static int GetMoney()
        {
            return PlayerPrefs.GetInt(k_currencyKey, 0);
        }
    }
}