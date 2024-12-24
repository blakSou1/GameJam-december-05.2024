using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class SaveAndLoadManager : MonoBehaviour
{ 
    internal List<SaveableObject> objects = new List<SaveableObject>();
    private string path;
    [SerializeField]private string filename;
    [SerializeField]private string defaultLevel;
    [SerializeField] private GameObject prefab;
    [SerializeField] private bool deleteSaves;
    private void Awake()
    {
       path = Application.streamingAssetsPath + $"/Saves(Client)/{filename}.xml";
       if(deleteSaves == true)
       {
           File.Delete(path);
           Debug.LogWarning("Delete Saves");
           deleteSaves = false;
       }
       Load();
    }

    public void Save()
    {
        XElement root = new XElement("root");

        foreach (SaveableObject obj in objects)
        {
            root.Add(obj.GetElement());
        }
        
        XDocument saveDocument = new XDocument(root);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + $"/Saves(Client)/");
        }
        File.WriteAllText(path, saveDocument.ToString());
        Debug.Log(path);
    }

    protected void Load() //Загрузка
    {
        XElement root = null;

        if (!File.Exists(path))
        {
            if (File.Exists(Application.streamingAssetsPath + $"/Saves(Default)/{defaultLevel}.xml"))
            {
                root = XDocument.Parse(File.ReadAllText(Application.streamingAssetsPath + $"/Saves(Default)/{defaultLevel}.xml")).Element("root");
            }

            if (root == null)
            {
                Debug.Log("Load Failed!");
                return;
            }
        }
        else
        {
            root = XDocument.Parse(File.ReadAllText(path)).Element("root");
        }
        
        GenerateScene(root);
    }
    
    private void GenerateScene(XElement root)
    {
        foreach (XElement instance in root.Elements("instance"))
        {
            Vector3 position = Vector3.zero;
            Vector3 size = Vector3.zero;
            Vector3 rotation = Vector3.zero;

            position.x = float.Parse(instance.Attribute("x")?.Value, CultureInfo.InvariantCulture);
            position.y = float.Parse(instance.Attribute("y")?.Value, CultureInfo.InvariantCulture);
            position.z = float.Parse(instance.Attribute("z")?.Value, CultureInfo.InvariantCulture);
            
            size.x = float.Parse(instance.Attribute("sizeX")?.Value, CultureInfo.InvariantCulture);
            size.y = float.Parse(instance.Attribute("sizeY")?.Value, CultureInfo.InvariantCulture);
            size.z = float.Parse(instance.Attribute("sizeZ")?.Value, CultureInfo.InvariantCulture);

            rotation.x = float.Parse(instance.Attribute("rotationX")?.Value, CultureInfo.InvariantCulture);
            rotation.y = float.Parse(instance.Attribute("rotationY")?.Value, CultureInfo.InvariantCulture);
            rotation.z = float.Parse(instance.Attribute("rotationZ")?.Value, CultureInfo.InvariantCulture);

            GameObject prefab = Resources.Load<GameObject>(instance.Value);
            if (prefab != null)
            {
                GameObject instanceObject = Instantiate(prefab, position, Quaternion.Euler(rotation));
                instanceObject.transform.localScale = size;
            }
            else
            {
                Debug.LogWarning($"Prefab not found: {instance.Value}");
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (deleteSaves == false)
        {
            Save();
        }
    }

}
