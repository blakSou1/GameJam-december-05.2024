using System.Xml.Linq;
using UnityEngine;



public class SaveableObject : MonoBehaviour
{
    [SerializeField] private string objectName;
    private float[] rotationsT = {0f, 90f, 180f, 270f, 360f};
    
    private SaveAndLoadManager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<SaveAndLoadManager>();
    }

    private void Start()
    {
        _manager.objects.Add(this);
    }

    private void OnDestroy()
    {
        _manager.objects.Remove(this);
    }

    public XElement GetElement()
    {
        XAttribute x = new XAttribute("x", transform.position.x);
        XAttribute y = new XAttribute("y", transform.position.y);
        XAttribute z = new XAttribute("z", transform.position.z);
        
        XAttribute sizeX = new XAttribute("sizeX", transform.localScale.x);
        XAttribute sizeY = new XAttribute("sizeY", transform.localScale.y);
        XAttribute sizeZ = new XAttribute("sizeZ", transform.localScale.z);
        
        XAttribute rotationX = new XAttribute("rotationX", transform.eulerAngles.x);
        XAttribute rotationY = new XAttribute("rotationY", transform.eulerAngles.y);
        XAttribute rotationZ = new XAttribute("rotationZ", transform.eulerAngles.z);
        
        XElement element = new XElement("instance", objectName, x,y,z, sizeX, sizeY, sizeZ, rotationX, rotationY, rotationZ);
        
        return element;
    }
}
