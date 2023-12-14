using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

namespace FragileReflection
{
    public static class CVSLoader
    {
        private const string url = "https://docs.google.com/spreadsheets/d/1Apa2_rSRNwJ7CI70f2WZwzwJdrZt7HRVeZyhK-KDvWw/export?format=csv";

        public static void DownloadTable(Action<string> onSheetLoadedAction)
        {
            DownloadRawCvsTable(url, onSheetLoadedAction);
        }

        private static async void DownloadRawCvsTable(string actualUrl, Action<string> callback)
        {
            HttpClient _httpClient = new HttpClient();
            var stringData = await _httpClient.GetStringAsync(actualUrl);
            callback(stringData);
            //using (UnityWebRequest request = UnityWebRequest.Get(actualUrl))
            //{
            //    yield return request.SendWebRequest();
            //    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError ||
            //        request.result == UnityWebRequest.Result.DataProcessingError)
            //    {
            //        Debug.LogError(request.error);
            //    }
            //    else
            //    {
            //        //if (_debug)
            //        //{
            //        //    Debug.Log("Successful download");
            //        //    Debug.Log(request.downloadHandler.text);
            //        //}

            //        callback(request.downloadHandler.text);
            //    }

            //}
            //yield return null;
        }
    }
}
