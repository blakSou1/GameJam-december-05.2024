using DG.Tweening;
using Player;
using System.Collections;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    internal float  _grapplingForce = 7;
    [SerializeField]private Vector3 _mousePos;
    private float camDepth;
    [SerializeField] GameObject prefab;
    internal GameObject currentGameObject;

    [SerializeField] private float speed = 20f;

    GameObject snar;

    [SerializeField]private Transform target;

    private Vector3 TT;

    Vector3 mainT;

    private void Update()
    {
        camDepth = Camera.main.nearClipPlane + 9f;
        _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDepth);
        target.position = Camera.main.ScreenToWorldPoint(_mousePos);
        if (Input.GetMouseButtonDown(0)) Shoot();
    }

    void Shoot()
    {       
        if(prefab.activeInHierarchy) Destroy(prefab);      
        prefab = Instantiate(prefab, transform.position, Quaternion.identity);

        DOTween.Sequence()
            .Append(prefab.transform.DOMoveX(target.position.x, 1f))
            .Join(prefab.transform.DOMoveY(target.position.y, 1f));
        
    }
}