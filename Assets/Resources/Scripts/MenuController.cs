using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SocialPlatforms.Impl;

public class MenuController : MonoBehaviour
{
    public Text scores;

    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/scores.veryspecificbinaryfiletype"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/scores.veryspecificbinaryfiletype", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            scores.text = "";
            save.scoreList.Sort(Comparer<Save.ScoreEntry>.Create(
                (x, y) => x.score.CompareTo(y.score))
            );
            string temp = "";
            for (int i = save.scoreList.Count-1; -1 < i && save.scoreList.Count - i-1  < 10; i--)
            {
                temp = temp +
                    save.scoreList[i].name +
                    save.scoreList[i].score + "\n";
            }
            scores.text = temp;
        }
        else
        {
            scores.text = "No previous scores saved";
        }
    }
}
