using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Helper
{
    public class InputHelper
    {
        public static bool OnUIMouseBtnDown(int button)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject btn = EventSystem.current.currentSelectedGameObject;
                if (btn != null)
                    return true;
                else
                    return false;
            }

            return false;
        } 

        public static bool OnGameMouseBtnDown(int button)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject btn = EventSystem.current.currentSelectedGameObject;
                if (btn == null)
                    return true;
                else
                    return false;
            }

            return false;

        }
    }
}
