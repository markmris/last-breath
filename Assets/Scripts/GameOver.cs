using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Scene gameScene;
    public Sprite playerJump;
    public CinemachineCamera cam;
    public InGameUIControl inGameUIControl;

    public void Start()
    {
        gameScene = SceneManager.GetSceneByName("Map");
    }

    public void ResetGame()
    {
        Destroy(cam);

        foreach (GameObject child in gameScene.GetRootGameObjects())
        {
            if (child.CompareTag("Zombie"))
            {
                ZombiePathfinding zombiePathfinding = child.GetComponent<ZombiePathfinding>();
                zombiePathfinding.walkSpeed = 0;
            }

            else if (child.name == "ZombieSpawns")
            {
                Destroy(child);
            }

            else if (child.CompareTag("AttackOrb"))
            {
                Destroy(child);
            }

            else if (child.CompareTag("Player"))
            {
                SpriteRenderer playerSprite = child.GetComponent<SpriteRenderer>();
                Rigidbody2D rigidBody = child.GetComponent<Rigidbody2D>();
                Destroy(child.GetComponent<BoxCollider2D>());
                Destroy(child.GetComponent<CircleCollider2D>());
                Destroy(GameObject.Find("SoundManager"));
                playerSprite.sprite = playerJump;
                playerSprite.sortingLayerName = "VFX";
                playerSprite.sortingOrder = 10;
                rigidBody.linearVelocity = new Vector2(0, 30);
            }
        }

        StartCoroutine(WaitBeforeReset());
        
    }

    IEnumerator WaitBeforeReset()
    {
        yield return new WaitForSeconds(1.5f);
        inGameUIControl.GameOver();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
        yield break;
    }
}
