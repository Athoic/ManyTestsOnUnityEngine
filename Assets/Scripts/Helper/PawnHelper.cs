using UnityEngine;

namespace Project.Helper
{
    public class PawnHelper
    {
        public static GameObject GetPawnBody(GameObject pawn)
        {
            return pawn.transform.GetChild(0).gameObject;
        }

        public static bool IsInCameraView(GameObject pawn)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(pawn.transform.position);
            if (screenPos.x >= 0 && screenPos.x <= Screen.width &&
                screenPos.y >= 0 && screenPos.y <= Screen.height)
                return true;
            else 
                return false;

        }



    }
}
