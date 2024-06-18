using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Level Configuration/New Level Configuration")]
public class LevelConfig : ScriptableObject
{
    public List<TileConfig> tiles;

    [System.Serializable]
    public class TileConfig
    {
        public int x, y;
        public List<ObjectConfig> objects;
    }

    [System.Serializable]
    public class ObjectConfig
    {
        public string objectType;
        public Direction direction;
        public ColorSet color;
    }
}