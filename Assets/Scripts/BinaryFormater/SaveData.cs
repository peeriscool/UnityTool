using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=5roZtuqZyuw
[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set { }
    }
    public EditorProfile profile;


    public void OnLoadSave()
    {
        SaveData.current = (SaveData)BinaryFormatManager.Load(Application.persistentDataPath + "/saves/save.save");
    }
}
