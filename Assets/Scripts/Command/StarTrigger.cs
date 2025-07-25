using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StarTrigger : MonoBehaviour
{
    public CommandManager cmdManager;
    public InputHandler inputHandler;
    public Player player;
    private Vector3 startPos;
    public float speed = 45f;


     public GameObject howToPlayImage;   
    public GameObject replayText;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        startPos = player.transform.position;

        howToPlayImage.SetActive(true);
        replayText.SetActive(false);
    }
     void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player")) return;

        inputHandler.enabled = false;

        StartCoroutine(Replay());
    }


    private IEnumerator Replay()
    {
        yield return new WaitForSeconds(0.3f);
        player.transform.position = startPos;

        yield return new WaitForSeconds(0.5f);

        howToPlayImage.SetActive(false);
        replayText.SetActive(true);

        foreach (var cmd in cmdManager.GetHistory())
        {
            cmd.Execute();
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}   
