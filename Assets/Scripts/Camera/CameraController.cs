using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject player;
    public PlayerMovement PlayerMovement;
    private Rigidbody2D PlayerRigidBody2d;
    public bool cameraMoving;
    [SerializeField] VectorStorage playerPositionStorage;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = player.GetComponent<PlayerMovement>();
        PlayerRigidBody2d = player.GetComponent<Rigidbody2D>();
        cameraMoving = false;
        if (playerPositionStorage.sceneName != SceneManager.GetActiveScene().name)
        {
            transform.position = new Vector3(playerPositionStorage.value.x, playerPositionStorage.value.y, -10);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.x < transform.position.x - 1f || player.transform.position.x > transform.position.x + 1f ||
            player.transform.position.y < transform.position.y - 1f || player.transform.position.y > transform.position.y + 1f)
        {
            cameraMoving = true;
            
        }
        if(PlayerRigidBody2d.velocity == Vector2.zero && cameraMoving && transform.position.x==player.transform.position.x && transform.position.y == player.transform.position.y) {
            cameraMoving=false;
        }
        
        if (cameraMoving)
        {
            Vector3 destination = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);                                            

            transform.position = Vector3.MoveTowards(transform.position, destination, (PlayerMovement.Speed/3.0f) * 3.8f * Time.deltaTime);
        }
        //transform.position = new Vector3(player.transform.position.x + Input.GetAxis("Horizontal") * 1f,
        //    player.transform.position.y + Input.GetAxis("Vertical") * 1f , transform.position.z);
    }
}
