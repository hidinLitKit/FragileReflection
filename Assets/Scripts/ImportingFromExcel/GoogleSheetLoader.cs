using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class GoogleSheetLoader : MonoBehaviour
    {
        public static void DownloadTable()
        {
            CVSLoader.DownloadTable(OnRawCVSLoaded);
        }

        private static void OnRawCVSLoaded(string rawCVSText)
        {
            SheetProccesor.ProcessData(rawCVSText);
        }
    }
}
