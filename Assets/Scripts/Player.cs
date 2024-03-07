using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    /*
    Los límites definidos con bound nos hacen falta debido a que el jugador se puede salir de la pantalla
    debido a que su rigidbody es quinemático, por lo que no se ve afectado por la gravedad ni puede colisionar
    con objetos estáticos.
    */
    [SerializeField] private float bound = 4.5f; // x axis bound 
    public float Timer, TimerAux;
    GameObject play;
    private Vector2 startPos; // Posición inicial del jugador
    public float initialScaleX, initialScaleY,initialScaleZ;
    public float temporalScaleX;
    public bool condicion=false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // Guardamos la posición inicial del jugador
        Timer = 0f;
        TimerAux = 10f;

        //condicion = false;
        //play = GameObject.FindGameObjectWithTag("Player");
        initialScaleX = transform.localScale.x;
        initialScaleY = transform.localScale.y;
        initialScaleZ = transform.localScale.z;
        temporalScaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
       PlayerMovement();
        if (Timer <= 0f && condicion)
        {

            Timer = TimerAux;

        }
            Timer -= Time.deltaTime;
        
        condicion = false;
        contraer();
       
        //play.transform.localScale = new Vector3(initialScaleX, initialScaleY, initialScaleZ);
    }

    void PlayerMovement()
    {
         float moveInput = Input.GetAxisRaw("Horizontal");
        // Controlaríamo el movimiento de la siguiente forma de no ser el rigidbody quinemático
        // transform.position += new Vector3(moveInput * speed * Time.deltaTime, 0f, 0f);

        Vector2 playerPosition = transform.position;
        // Mathf.Clamp nos permite limitar un valor entre un mínimo y un máximo
        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * speed * Time.deltaTime, -bound, bound);
        transform.position = playerPosition;
    }

    public void ResetPlayer()
    {
        transform.position = startPos; // Posición inicial del jugador
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {

        if (collision.CompareTag("powerUp")) // Si colisionamos con un powerUp
        {
            if (collision.GetComponent<SpriteRenderer>().sprite.name == "Heart")
            {
                GameManager.Instance.AddLife(); // Añadimos una vida
            }
            if (collision.GetComponent<SpriteRenderer>().sprite.name == "BlackHearth")
            {
                GameManager.Instance.LoseLife();
            }
            if (collision.GetComponent<SpriteRenderer>().sprite.name == "ball")
            {
                Debug.Log("Entra");
                alargar();
                
            }





            Destroy(collision.gameObject); // Lo destruimos
        }

    }

    public  void alargar()
    {
        if (!condicion)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 2 , gameObject.transform.localScale.y , gameObject.transform.localScale.z);
            condicion = true;
            
        }
        else
        {
            Timer = TimerAux;
        }
        
    }
    public void contraer()
    {
        gameObject.transform.localScale = new Vector3(initialScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
}
