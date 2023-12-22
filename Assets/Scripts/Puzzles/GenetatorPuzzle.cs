using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FragileReflection
{
    public class GenetatorPuzzle : Interactable, IDataPersistence
    {
        [SerializeField] private string unsolvedMes;
        [SerializeField] private string solvedMes;
        [SerializeField] private Light _ligth;
        [SerializeField] private Transform _shutter;
        [SerializeField] private string _id;

        private bool isSolved = false;
        
        [ContextMenu("Generate id")]
        private void GenerateGuid()
        {
            _id = System.Guid.NewGuid().ToString();
        }
        private void OnValidate()
        {
            if (_id == string.Empty)
            {
                Debug.LogWarning("Object" + gameObject.name + "has not been initialized with save value");
            }
        }
        private void Awake()
        {
            ObjectState.LayerChange(gameObject, ObjectState.InteractableLayer);
            StartCoroutine(Pulsation());
        }
        public override string GetDescription()
        {
            return !isSolved ? unsolvedMes : solvedMes;
        }

        public override void Interact()
        {
            Solve();
        }
        private void setLight()
        {
            _ligth.color = Color.green;
            StopCoroutine(Pulsation());
            _ligth.intensity = 1f;
        }
        IEnumerator Pulsation()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.7f);
                _ligth.DOIntensity(2f, 0.7f);
                yield return new WaitForSeconds(0.7f);
                _ligth.DOIntensity(1f, 0.7f);
            }
        }
        private void Solve()
        {
            ObjectState.LayerChange(gameObject, ObjectState.DefaultLayer);
            setLight();
            _shutter.DOLocalMoveY(-3.2f, 20f);
        }

        public void LoadData(GameData data)
        {
            string objectDataString;
            List<string> objectData;

            if(!data.PuzzleData.TryGetValue(_id, out objectDataString)) return;

            objectData = objectDataString.Split(',').Select(s => s).ToList();
            if (isSolved = bool.Parse(objectData[0])) Solve();

            
        }
        public void SaveData(GameData data)
        {
            string objectDataString;
            List<string> objectData;

            if (data.PuzzleData.ContainsKey(_id)) data.PuzzleData.Remove(_id);

            objectData = new List<string>() { isSolved.ToString()};
            objectDataString = string.Join(",", objectData.Select(b => b.ToString()).ToArray());

            data.PuzzleData.Add(_id, objectDataString);

        }
    }
}
