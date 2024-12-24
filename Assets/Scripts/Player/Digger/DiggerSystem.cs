using System.Collections;
using UnityEngine;

namespace Player
{
    public class DiggerSystem : MonoBehaviour
    {
        [SerializeField]private float destination;
        [SerializeField]private Transform[] points;

        private Coins _coinClass;
        
        private RaycastHit _hitdown;
        private Ray _raydown;
        
        private RaycastHit _hitright;
        private Ray _rayright;
        
        private RaycastHit _hitleft;
        private Ray _rayleft;
        
        private ParticleSystem _ps;

        private ItemScriptableObject item;

        private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");

        [SerializeField] private Animator anim;

        [SerializeField] private AudioClip[] sounds;
        [SerializeField] private AudioSource audioSrc;

        [Header("����� ���� ������������ �����")]
        [SerializeField] private float speedDestroybalBlock = 0.01f;
        [Header("������� ������������� ����� �� ���")]
        [SerializeField] private float tickedParamDestroybal = 0.001f;

        [SerializeField] private string nameShader = "ShaderBlock";
        [SerializeField] private string nameParam = "_Dissolve";

        private void Start()
        {
            _ps = GetComponentInChildren<ParticleSystem>();
            _coinClass = FindFirstObjectByType<Coins>();
        }
        private void Update()
        {
            _raydown = new Ray(points[0].position, Vector3.down);
            _rayleft = new Ray(points[1].position, Vector3.left);
            _rayright = new Ray(points[2].position, Vector3.right);

            Debug.DrawRay(points[0].position, Vector3.down*destination);
            Debug.DrawRay(points[1].position, Vector3.right*destination);
            Debug.DrawRay(points[2].position, Vector3.left*destination);
            
            Dig();
        }

        private void Audio()
        {
            audioSrc.PlayOneShot(sounds[Random.Range(0, sounds.Length)], 1f);
        }
        private IEnumerator BlockDestroybal(GameObject block)
        {
            Material mat = block.GetComponent<MeshRenderer>().material;//�������� �������� ����� ��� ���� � ����� �������
            if (mat.shader.name.Contains(nameShader))//��������� ����� �� �� ��������� ����� ������ ������
            {
                float param = mat.GetFloat(nameParam);//��������� ���������� ������� ������������

                while (true)
                {
                    param -= tickedParamDestroybal;
                    mat.SetFloat(nameParam, param);//������� ���������

                    if (param >= 0.2f) yield return new WaitForSeconds(speedDestroybalBlock);
                    else break;
                }
            }

            item = block.GetComponent<ItemObject>().item;
            GetCoinFromBlock();
            Destroy(block);
        }
        private void Diggers(RaycastHit hit, Ray ray)
        {
            if(Physics.Raycast(ray, out hit, destination, 1 << 6))
            {
                GameObject lastBlock = hit.collider.gameObject;

                Audio();//�������� ������

                anim.CrossFade(AttackState, 0.1f, 0, 0);//�������� �����(�����) ������

                StartCoroutine(BlockDestroybal(lastBlock));//�������� ����� + ��� �������� + ���������� �����

                PlayParticleSystem(_ps);//�������� ��� ����������� �����
            }
        }
        private void Dig()
        {
            if (Input.GetKeyDown(KeyCode.S)) Diggers(_hitdown, _raydown);
            else if (Input.GetKeyDown(KeyCode.D)) Diggers(_hitright, _rayright);
            else if (Input.GetKeyDown(KeyCode.A)) Diggers(_hitleft, _rayleft);
        }
        private void PlayParticleSystem(ParticleSystem ps)
        {
            ps.Play();
        }
        private void GetCoinFromBlock()
        {
            if(_coinClass != null) _coinClass.coins += item.cost;
        }
    }
}