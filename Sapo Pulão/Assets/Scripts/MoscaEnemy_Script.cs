using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoscaEnemy_Script : MonoBehaviour
{
    [SerializeField] float velocidade;
    [SerializeField] bool IsMoscaMestre;
    [SerializeField] SpriteRenderer sprMosca;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null)
        {
            if (IsMoscaMestre)
            {
                velocidade = Mathf.Lerp(1.5f, 3.5f, Math.Clamp(Mathf.Abs(player.transform.position.x-transform.position.x), 1, 15)/15);
                Debug.Log(Vector2.Distance(player.transform.position, transform.position));
            }
            Vector3 direction = (player.transform.position-transform.position).normalized*velocidade*Time.fixedDeltaTime;
            transform.position += direction; 
            if(direction.x < 0)
            {
                sprMosca.flipX = true;
            }
            else
            {
                sprMosca.flipX = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D cool)
    {
        if(cool.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }
}
