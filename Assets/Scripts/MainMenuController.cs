using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioSource sfxPlayer;
    public LayerMask barrierLayer;
    public Rigidbody2D player;
    public Rigidbody2D zombie;
    public GameObject mods;
    public float walkSpeed;
    public int playerDirection = -1;
    public int zombieDirection = 1;

    public void OnStartClicked()
    {
        mods.transform.SetParent(null, true);
        DontDestroyOnLoad(mods);
        sfxPlayer.Play();
        SceneManager.LoadScene("Map");
    }

    public void OnExitClicked()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    void FixedUpdate()
    {
        player.linearVelocity = new Vector2(playerDirection * walkSpeed, player.linearVelocityY);
        zombie.linearVelocity = new Vector2(zombieDirection * walkSpeed, zombie.linearVelocityY);

        RaycastHit2D raycast = Physics2D.Raycast(player.transform.position, new Vector2(playerDirection, 0), 1, barrierLayer);

        if (raycast)
        {
            playerDirection *= -1;
            zombieDirection *= -1;
        }
    }
}