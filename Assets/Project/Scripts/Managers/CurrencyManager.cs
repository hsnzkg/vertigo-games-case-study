using UnityEngine;

namespace Project.Scripts.Managers
{
    public static class CurrencyManager
    {
        private const string k_currencyKey = "UserCurrency";
        private static int s_currency;

        static CurrencyManager()
        {
            LoadCurrency();
        }

        private static void LoadCurrency()
        {
            s_currency = PlayerPrefs.GetInt(k_currencyKey, 0);
        }

        public static bool HasCurrency(int amount)
        {
            return s_currency >= amount;
        }

        public static void AddCurrency(int amount)
        {
            s_currency += amount;
            SaveCurrency();
        }
        
        public static void SetCurrency(int amount)
        {
            s_currency = amount;
            SaveCurrency();
        }

        public static bool SubtractCurrency(int amount)
        {
            if (!HasCurrency(amount)) return false;

            s_currency -= amount;
            SaveCurrency();
            return true;
        }

        private static void SaveCurrency()
        {
            PlayerPrefs.SetInt(k_currencyKey, s_currency);
            PlayerPrefs.Save();
        }
    }
}