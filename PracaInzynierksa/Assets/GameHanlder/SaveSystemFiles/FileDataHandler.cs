using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Specialized;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //Czytamy zserializowane dane z pliku
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //Deserializujemy Jesona do obiektu C#
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;

    }

    public void Save(GameData data)
    {
        //Uzywamy PathCombine zeby laczenie sciezki bylo kompatybilne na roznych systemach operacyjnych
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //Tworzymy folder jesli nie istnieje w sciezce
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //Serializujemy dane C# na Jsona 
            string dataToStore = JsonUtility.ToJson(data, true);
            //Zapisujemy do pliku
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" +e);
        }
    }
}
