using System;
using UnityEngine;
using DG.Tweening;
namespace Player
{
    public class PlayerMovements : MonoBehaviour
    {
        private Rigidbody rb;
        [Header("Player Settings")]
        [SerializeField]private float _speed;
        [SerializeField]private GameObject[] _poolObjects;
        [SerializeField]private int tntcolvo;
        [SerializeField] private Transform explosiveTransform;
        float horizontalInput = 0f;
        
        internal GameObject currentGameObject;
        
        private Vector3 _direction;
        
        [Header("Animator Settings")]
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private Animator _animator;
        internal static bool isLocked;
        private Vector3 targetScale;
        private Quaternion targetRotation;

        [Header("Audio")]
        [SerializeField] private AudioClip sounds;

        [SerializeField]private ExplosiveClass _explosiveClass;

        [SerializeField] private AudioSource audioSrcMove;

        

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            currentGameObject = this.gameObject;
            targetScale = transform.localScale;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.D)) horizontalInput = 1f * _speed;
            else if (Input.GetKey(KeyCode.A)) horizontalInput = -1f * _speed;
            else horizontalInput = 0f;

            _direction.x = horizontalInput;
            
            if(horizontalInput != 0f)
            {
                if(audioSrcMove != null && !_animator.GetBool("mov"))
                    audioSrcMove.Play();
                _animator.SetBool("mov", true);
            }
            else
            {
                _animator.SetBool("mov", false);
                audioSrcMove.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S) & tntcolvo > 0)
            {
                SpawnExplosive(_explosiveClass);
                tntcolvo -= 1;
            }

            HandleRotation();
        }

        private void HandleRotation()
        {
            if (horizontalInput < 0)
                targetRotation = Quaternion.Euler(0, 270, 0);
            else if (horizontalInput > 0)
                targetRotation = Quaternion.Euler(0, 90, 0);
            else
                return;

            Quaternion globalRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0) * targetRotation;
            spriteTransform.rotation = Quaternion.Slerp(spriteTransform.rotation, globalRotation, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("DungeManagerEnable") & Input.GetKeyDown(KeyCode.E))
            {
                ShowDungeManager();
            }
        }
        
        private void FixedUpdate()
        {
            Move();
        }
        
        private void Move() {if(isLocked != true) {rb.velocity = new Vector2(_direction.x, rb.velocity.y);}}
        public void TranslatePlayer(Transform transform) => transform.position = Vector3.zero;

        private void ShowDungeManager() => _poolObjects[0].SetActive(true);

        private void SpawnExplosive(ExplosiveClass explosive)
        {
            if (Dynamit.isWorked >= 1)
            {
                Instantiate(explosive.gameObject, explosiveTransform.position, Quaternion.identity);
            }
            
        }
    }

}
