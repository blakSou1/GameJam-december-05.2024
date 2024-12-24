using Player;
using System.Collections;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    internal float  _grapplingForce = 7;
    private Vector3 _mousePos;
    private float camDepth;
    [SerializeField] GameObject prefab;
    internal GameObject currentGameObject;

    [SerializeField] private float speed = 20f;
    [SerializeField] GameObject mesh;
    [SerializeField] GameObject blesk;

    GameObject snar;

    private Vector3 target;

    private Vector3 TT;

    Vector3 mainT;


    bool activetB = false;
    private void Start()
    {
        snar = Instantiate(prefab);

        blesk.SetActive(false);
    }

    bool yes = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            snar.SetActive(false);

            activetB = false;

            yes = false;

            mainT = transform.position + new Vector3(0, 0.25f, 0);

            _mousePos = Input.mousePosition;
            camDepth = Camera.main.nearClipPlane + 9;
            Vector3 posMid = new Vector3(_mousePos.x, _mousePos.y, camDepth);

            target = Camera.main.ScreenToWorldPoint(posMid);
            if (target.z < 0 | target.z > 0)
            {
                target.z = 0;
            }
            target += new Vector3(0, 0.25f, 0);

            Plu();
        }
        else if(Input.GetKey(KeyCode.Mouse1) && activetB)
        {
            if (!yes)
            {
                yes = true;

                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<PlayerMovements>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                mesh.SetActive(false);
                blesk.SetActive(true);
            }

            float dist = Vector3.Distance(TT, transform.position);
            if (dist >= 0.2f)
            {
                transform.position =
Vector3.MoveTowards(transform.position, TT, speed * Time.deltaTime);
            }
            else
            {
                activetB = false;
            }
        }
        else if(!Input.GetKey(KeyCode.Mouse1) && activetB && yes || !activetB && yes)
        {
            snar.SetActive(false);

            activetB = false;
            yes = false;

            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<PlayerMovements>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            mesh.SetActive(true);
            blesk.SetActive(false);
        }
    }
    private void Plu()
    {
        // Получаем направление к цели
        Vector3 direction = (target - mainT).normalized;

        // Создаем снаряд
        snar.transform.position = mainT;
        snar.SetActive(true);

        snar.GetComponent<Bullets>().grap = this;
        snar.transform.rotation = Quaternion.identity;

        // Получаем компонент Rigidbody снаряда и добавляем силу для его перемещения
        Rigidbody rb = snar.GetComponent<Rigidbody>();
        rb.AddForce(direction * _grapplingForce, ForceMode.Impulse);
    }
    public void PlayerStart(Vector3 i)
    {
        activetB = true;

        TT = i;
    }
}