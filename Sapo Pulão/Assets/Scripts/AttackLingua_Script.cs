using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackLingua_Script : MonoBehaviour
{
    [SerializeField] Transform Parent;
    [SerializeField] LineRenderer Lingua;
    [SerializeField] LayerMask layer;
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioClip[] audios;
    public bool IsAttack = false;
    Vector2 TeclaID;
    GameObject alvoSave;

    public void OnAttack(InputAction.CallbackContext context){
        TeclaID = context.ReadValue<Vector2>();

        if (context.performed && !IsAttack)
        {
            //VERIFICAR REGIÃO CLICADA PRA VER SE TEM ALGUM INIMIGO
            var alvo = Physics2D.OverlapCircle(Parent.position+(Vector3)TeclaID*2.5f, 1.2f, layer);
            if(alvo != null)
            {
                StartCoroutine(Linguada(alvo.gameObject));
            }
            else
            {
                StartCoroutine(Linguada());
            }
            SFX.PlayOneShot(audios[0]);
        }
    }

    IEnumerator Linguada(GameObject alvo = null)
    {
        IsAttack = true;
        if(alvo != null)
        {
            Debug.Log("Linguei coisa");
            alvoSave = alvo;
            //ESPERAR UM TEMPO
            Destroy(alvo, 0.2f);
            yield return new WaitForSeconds(0.3f);
            SFX.PlayOneShot(audios[1]);
            
            //RETORNAR A POSIÇÃO PADRÃO
            if (IsAttack)
            {
                IsAttack = false;
                Lingua.SetPosition(1, new Vector3(0, -1, -2));
                alvoSave = null;
            }
        }
        else
        {
            Debug.Log("Linguei nada");
            Lingua.SetPosition(1, (Vector3)TeclaID*8.5f);

            yield return new WaitForSeconds(0.1f);
            
            alvoSave = null;
            Lingua.SetPosition(1, new Vector3(0, -1, -2));
            IsAttack = false;
        }
    }

    void Update()
    {
        if (IsAttack && alvoSave != null)
        {
            Lingua.SetPosition(1, Parent.transform.InverseTransformPoint(alvoSave.transform.position));
            if(Vector2.Distance(Lingua.GetPosition(0), Lingua.GetPosition(1)) > 15)
            {
                IsAttack = false;
                Lingua.SetPosition(1, new Vector3(0, -1, -2));
                alvoSave = null;
            }
        }
    }
}
