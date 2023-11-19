using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Letter : NarrativeTalker
    {
        public static event System.Action<bool> letterAction;
        public void letterOpen()
        {
           InputManager.ToogleActionMaps(InputManager.inputActions.UI);
           letterAction?.Invoke(false);
        }
        public void letterClose()
        {
            letterAction?.Invoke(true);
        }
        public void buttonNext()
        {

        }
        public void buttonBack() 
        {
            
        }
        private void updateUI()
        {
            
        }
    }
}
