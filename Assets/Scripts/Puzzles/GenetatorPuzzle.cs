using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace FragileReflection
{
    public class GenetatorPuzzle : Interactable
    {
        [SerializeField] string unsolvedMes;
        [SerializeField] string solvedMes;
        [SerializeField] private Light _ligth;
        [SerializeField] private Transform _shutter;
        private bool isSolved = false;
        private int _interLayer = 6;
        private void Awake()
        {
            gameObject.layer = _interLayer;
            StartCoroutine(Pulsation());
        }
        public override string GetDescription()
        {
            return !isSolved ? unsolvedMes : solvedMes;
        }

        public override void Interact()
        {
            gameObject.layer = 0;
            setLight();
            _shutter.DOLocalMoveY(-3.2f, 20f);

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
        


    }
}
