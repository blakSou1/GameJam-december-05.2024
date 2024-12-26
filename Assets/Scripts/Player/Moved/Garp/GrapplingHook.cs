using DG.Tweening;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    internal float  _grapplingForce = 7;   
    private float camDepth;
    private GameObject player;
    [SerializeField] private Vector3 _mousePos;
    [SerializeField] GameObject prefab;
    [SerializeField] private float speed = 20f;
    [SerializeField] private Transform target;  
    [SerializeField] private Transform grapplePoint;
    [SerializeField] private float maxDistance;
    public static bool _holdAllow;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        camDepth = Camera.main.nearClipPlane + 10f;
        _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDepth);
        target.position = Camera.main.ScreenToWorldPoint(_mousePos);
        
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) Shoot(); 
        if(Input.GetMouseButton(0) & _holdAllow)Hold();
        if(Input.GetMouseButtonDown(1)) Animate();
    }

    private void Hold()
    {
        prefab.transform.Translate(grapplePoint.right * speed);
        float distance = Vector3.Distance(new Vector3(prefab.transform.position.x, prefab.transform.position.y, 0f), new Vector3(transform.position.x, transform.position.y, 0f));
        if(distance >= maxDistance)
        {
            _holdAllow = false;
        }
    }

    void Shoot()
    {       
        if(prefab.activeInHierarchy) Destroy(prefab);        
        Vector3 direction = target.position - grapplePoint.position;   
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 ea = grapplePoint.eulerAngles;
        ea.z = angle + 0f;
        grapplePoint.eulerAngles = ea;
        _holdAllow = true;
        prefab = Instantiate(prefab, grapplePoint.position, Quaternion.identity);
    }
    void Animate()
    {
        DOTween.Sequence()
            .Append(player.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1f))
            .Append(player.transform.DOMoveX(prefab.transform.position.x, 1f))
            .Join(player.transform.DOMoveY(prefab.transform.position.y, 1f))         
            .Append(player.transform.DOScale(new Vector3(0.7f, 0.83f, 1f), 1f));
    }

    
}