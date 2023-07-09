using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{

    public class SwitchButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
       public Rewinder up_down;
        int swaprewindint,temp1;
        [SerializeField] int up_dwn = 0;//set 0 for upper world(W) and 1 for Lower world (S);
        private void Start()
        {
            swaprewindint = 0;
            temp1 = 0;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            
            temp1 = 0;
            SendValueToControl(0.0f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(temp1 ==0)
            {
                temp1 = 1;
                if (up_down.tempactiverewindmanagerindex == 0)
                    swaprewindint = 1;
                else
                    swaprewindint = 0;
            }

            if (up_dwn == 0)
            {
                if(swaprewindint ==0)
                {
                    SendValueToControl(1.0f);
                }
                else if(swaprewindint == 1)
                {
                    SendValueToControl(0.0f);
                }
            }
            else
            {
                if (swaprewindint == 0)
                    SendValueToControl(0.0f);

                else if (swaprewindint == 1)
                    SendValueToControl(1.0f);
            }    
        }

   
        [InputControl(layout = "Button")]
        [SerializeField]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }
}

