using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryData : ScriptableObject
{
    public List<Param> param = new List<Param>();

    [System.Serializable]
    public class Param
    {
        public int id;
        public string mainText;
        public Sprite sprite;
    }
}
