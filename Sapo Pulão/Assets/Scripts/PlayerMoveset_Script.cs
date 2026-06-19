using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMoveset_Script : MonoBehaviour
{
    [SerializeField] float H_Force;
    [SerializeField] Vector2 Força_AtualMAX;
    [SerializeField] Vector3 Offset;
    [SerializeField] Animator animeSapo;
    [SerializeField] float velocity;
    public Transform CameraFoco;
    public Rigidbody2D fisicaSapo;
    public float TimeDelayJump;
    int direction = 0;
    bool ReleasedSpace = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ReleasedSpace && Força_AtualMAX[0] == 0)
        {
            fisicaSapo.position += Vector2.right*direction*velocity*Time.fixedDeltaTime;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Apertei :)");
            if(Força_AtualMAX[0] == 0 && !ReleasedSpace)
            {
                animeSapo.speed = 1/(TimeDelayJump*Força_AtualMAX[1]);
                animeSapo.Play("Carregando_Pulo");
            }
            StartCoroutine(ChangeForce());
        }
        if (context.canceled)
        {
            Debug.Log("Soltei :(");
            animeSapo.speed = 1;
            if(Força_AtualMAX[0] != 0)
            {
                //INTERROMPER LOOP
                ReleasedSpace = true;
                StopCoroutine(ChangeForce());

                //PULAR
                Debug.Log("Pulei com força: " + Força_AtualMAX[0].ToString());
                fisicaSapo.AddForce(new Vector2(H_Force, Força_AtualMAX[0]*2), ForceMode2D.Impulse);
                animeSapo.Play("PulandoA");

                //ZERAR FORÇA
                Força_AtualMAX[0] = 0;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = (int)context.ReadValue<Vector2>().x;
        if(direction != 0)
        {
            Debug.Log(math.clamp(direction, -0.3f, 0.3f));
            this.transform.localScale = new Vector3(math.clamp(direction, -0.3f, 0.3f), 0.3f, 0.3f);
        }
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if(colisao.gameObject.tag == "Chao")
        {
            Debug.Log("No chao");
            ReleasedSpace = false;
            animeSapo.Play("PulandoB");

            //MUDAR FOCO DA CAMERA
            CameraFoco.position = colisao.gameObject.transform.position+Offset;
        }
        if(colisao.gameObject.tag == "Respawn")
        {
            SceneManager.LoadScene(0);
        }
        Debug.Log(colisao.gameObject.name);
    }

    IEnumerator ChangeForce()
    {
        while(!ReleasedSpace)
        {
            Debug.Log("Mudou");
            Força_AtualMAX[0] = Math.Clamp(Força_AtualMAX[0]+1, 0, Força_AtualMAX[1]);
            yield return new WaitForSeconds(TimeDelayJump);
        }
    }
}
