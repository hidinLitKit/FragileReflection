using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using WeaponSystem;

namespace FragileReflection
{
    public class SheetProccesor : MonoBehaviour
    {
        private const int _id = 0;
        private const int _headDamage = 1;
        private const int _bodyDamage = 2;
        private const int _rateOfFire = 3;
        private const int _rechargeSpeed = 4;
        private const int _recoilRadius = 5;
        private const int _magazine = 6;

        private const char _cellSeporator = ',';
        private const char _inCellSeporator = ';';

        public static void ProcessData(string cvsRawData)
        {
            char lineEnding = GetPlatformSpecificLineEnd();
            string[] rows = cvsRawData.Split(lineEnding);
            int dataStartRawIndex = 1;

            for (int i = dataStartRawIndex; i < rows.Length; i++)
            {
                Debug.Log(rows[i]);
                string[] cells = rows[i].Split(_cellSeporator);
                ScriptableWeapon weapon = Exporting.scriptableObjects[i-1];

                weapon.HeadDamage = ParseFloat(cells[_headDamage]);
                weapon.BodyDamage = ParseFloat(cells[_bodyDamage]);
                weapon.RateOfFire = ParseFloat(cells[_rateOfFire]);
                weapon.RechargeSpeed = ParseFloat(cells[_rechargeSpeed]);
                weapon.RecoilRadius = ParseFloat(cells[_recoilRadius]);
                weapon.Magazine = ParseInt(cells[_magazine]);
            }

        }

        private static int ParseInt(string s)
        {
            int result = -1;
            if (!int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.Log("Can't parse int, wrong text");
            }

            return result;
        }

        private static float ParseFloat(string s)
        {
            float result = -1;
            if (!float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.Log("Can't pars float,wrong text ");
            }

            return result;
        }

        private static char GetPlatformSpecificLineEnd()
        {
            char lineEnding = '\n';
#if UNITY_IOS
        lineEnding = '\r';
#endif
            return lineEnding;
        }
    }
}
