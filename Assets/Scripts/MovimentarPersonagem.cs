using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPersonagem : MonoBehaviour
{
    public CharacterController controle;
    public float velocidade = 6f;
    public float alturaPulo = 6f;
    public float gravidade = -20f;

    public Transform checaChao;
    public float raioEsfera = 0.4f;
    public LayerMask chaoMask;
    public bool estaNoChao;

    Vector3 velocidadeCai;


    // Start is called before the first frame update
    void Start()
    {
        controle = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //cria uma esfera de raioEsfera na posicao do checaChao, batendo com a mascara do chao
        // se estah em contato com o chaoMask, entao retorna true

        estaNoChao = Physics.CheckSphere(checaChao.position, raioEsfera, chaoMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;

        controle.Move(mover * velocidade * Time.deltaTime);

        if(estaNoChao && Input.GetButtonDown("Jump"))
        {
            velocidadeCai.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }
        if(!estaNoChao)
        {
            velocidadeCai.y += gravidade * Time.deltaTime * 2; //*2 pra ficar mais pesado o pulo
        }

        controle.Move(velocidadeCai * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(checaChao.position, raioEsfera);
    }
}
