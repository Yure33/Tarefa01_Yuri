using UnityEngine;

public class Chao_Script : MonoBehaviour
{
    bool PlayerChegou = false;
    bool PossoDestruir = false;
    Vector3 Offset;
    float tempoDestruir = 2f;
    public GameObject SelfOBJ;
    public GameObject Mosca;
    public Vector2 Min_Max_Distance;

    void Start()
    {
        Offset = Vector3.right*Random.Range(Min_Max_Distance[0], Min_Max_Distance[1]);
    }

    void Update()
    {
        if (PossoDestruir)
        {
            tempoDestruir -= Time.deltaTime;
            if(tempoDestruir <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D cool)
    {
        if(cool.gameObject.name == "SapoOBJ")
        {
            if (!PlayerChegou)
            {
                if (SelfOBJ != null)
                {
                    Instantiate(SelfOBJ, this.gameObject.transform.position+Offset, this.gameObject.transform.rotation);
                }
                if(Mosca != null)
                {
                    Vector3 moscaPosicao = new Vector3(Offset.x*2, 10, 0);
                    Instantiate(Mosca, this.gameObject.transform.position+moscaPosicao, this.gameObject.transform.rotation);
                }

                PlayerChegou = true; 
            }
            else
            {
                PossoDestruir = false;
                tempoDestruir = 2f;
            }
        }
    }
    void OnCollisionExit2D(Collision2D cool)
    {
        if(cool.gameObject.name == "SapoOBJ")
        {
            PossoDestruir = true;
        }
    }
}
