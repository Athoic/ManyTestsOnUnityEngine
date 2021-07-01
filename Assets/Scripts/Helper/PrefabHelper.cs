using UnityEngine;

namespace Project.Helper
{
    public class PrefabHelper
    {
        private const string DEFAULT_PATH = "Prefabs\\";
        public static GameObject GetPrefabInGameObject(string prefabPath)
        {
            string path = DEFAULT_PATH + prefabPath;
            return (GameObject)Resources.Load(path);
        }

        private const string BATTLE_PAWN_LAYER = "BattlePawns";
        public static GameObject InstantiateBattlePawn(GameObject gameObject)
        {
            GameObject layer= GameObject.Find(BATTLE_PAWN_LAYER);
            GameObject instance= Object.Instantiate(gameObject, layer.transform);
            return instance;

        }
    }
}
