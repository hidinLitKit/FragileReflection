using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Letter : NarrativeTalker
    {
        public void letterOpen()
        {
           InputManager.ToogleActionMaps(InputManager.inputActions.UI);
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
