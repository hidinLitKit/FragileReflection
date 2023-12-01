using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{

    public class ExcelExport : MonoBehaviour
    {
        [SerializeField] private List<ScriptableWeapon> scriptableObjects = new List<ScriptableWeapon>();

        private void OnValidate()
        {
            Exporting.scriptableObjects = scriptableObjects;
        }
    }

    public static class Exporting
    {
        public static List<ScriptableWeapon> scriptableObjects;
    }
}
