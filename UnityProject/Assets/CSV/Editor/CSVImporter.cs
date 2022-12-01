using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVImporter : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        string targetFile = "Assets/CSV/Story.csv";
        string exportFile = "Assets/CSV/Story.asset";

        foreach (string asset in importedAssets)
        {
            if (!targetFile.Equals(asset)) continue;

            StoryData sd = AssetDatabase.LoadAssetAtPath<StoryData>(exportFile);

            if (sd == null)
            {
                sd = ScriptableObject.CreateInstance<StoryData>();
                AssetDatabase.CreateAsset((ScriptableObject)sd, exportFile);
            }

            sd.param.Clear();

            using (StreamReader sr = new StreamReader(targetFile))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');

                    StoryData.Param p = new StoryData.Param();
                    p.id = int.Parse(values[0]);
                    p.mainText = values[1];
                    p.sprite = Resources.Load<Sprite>(values[2]);
                    sd.param.Add(p);
                }

                AssetDatabase.SaveAssets();

                Debug.Log("Data updated");
            }
        }
        {

        }
    }
}
