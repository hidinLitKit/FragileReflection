using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FragileReflection
{
    public class AudioButton : MonoBehaviour
    {
        [SerializeField] private AudioSource _mainFx;
        [SerializeField] private AudioClip _hoverFx;

        //private void Start()
        //{
        //    Button[] buttons = GetComponentsInChildren<Button>();

        //    foreach (Button button in buttons)
        //    {
        //        // Добавляем обработчик события наведения мыши
        //        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
        //        EventTrigger.Entry pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        //        pointerEnter.callback.AddListener((eventData) => { HoverSound(); });
        //        trigger.triggers.Add(pointerEnter);

        //        // Добавляем обработчик события нажатия мыши
        //        EventTrigger.Entry pointerClick = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        //        pointerClick.callback.AddListener((eventData) => { ClickSound(); });
        //        trigger.triggers.Add(pointerClick);
        //    }
        //}

        public void HoverSound()
        {
            _mainFx.PlayOneShot(_hoverFx);
        }
    }
}
