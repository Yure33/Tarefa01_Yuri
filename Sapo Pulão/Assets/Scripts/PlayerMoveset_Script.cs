using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMoveset_Script : MonoBehaviour
{
    [SerializeField] float H_Force;
    [SerializeField] Vector2 Força_AtualMAX;
    [SerializeField] Vector3 Offset;
    public Transform CameraFoco;
    public Rigidbody2D fisicaSapo;
    bool ReleasedSpace = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //NAO SEI UTILIZAR O NEW INPUT SYSTEM :(
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Apertei :)");
            StartCoroutine(ChangeForce());
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("Soltei :(");
            if(Força_AtualMAX[0] != 0)
            {
                //INTERROMPER LOOP
                ReleasedSpace = true;
                StopCoroutine(ChangeForce());

                //PULAR
                Debug.Log("Pulei com força: " + Força_AtualMAX[0].ToString());
                fisicaSapo.AddForce(new Vector2(H_Force, Força_AtualMAX[0]*2), ForceMode2D.Impulse);

                //ZERAR FORÇA
                Força_AtualMAX[0] = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if(colisao.gameObject.tag == "Chao")
        {
            Debug.Log("No chao");
            ReleasedSpace = false;

            //MUDAR FOCO DA CAMERA
            CameraFoco.position = colisao.gameObject.transform.position+Offset;
        }
    }

    IEnumerator ChangeForce()
    {
        while(!ReleasedSpace)
        {
            Debug.Log("Mudou");
            Força_AtualMAX[0] = Math.Clamp(Força_AtualMAX[0]+1, 0, Força_AtualMAX[1]);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
