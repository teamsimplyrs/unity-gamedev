using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] string destinationScene;
    [SerializeField] Vector2 playerPosition;
    [SerializeField] VectorStorage playerPositionStorage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPositionStorage.value = playerPosition;
            playerPositionStorage.sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(destinationScene);
        }
    }
}
