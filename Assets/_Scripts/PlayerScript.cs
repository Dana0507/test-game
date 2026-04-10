using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;
    private int collectedCount = 0;
    private bool canMove = true;
    
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip spaceSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = spaceSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        if (!canMove) return;

        float yInput = Input.GetAxisRaw("Vertical");
        float newY = transform.position.y + (yInput * speed * Time.deltaTime);
        newY = Mathf.Clamp(newY, minY, maxY);

        transform.position = new Vector2(transform.position.x, newY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            collectedCount++;
            audioSource.PlayOneShot(collectSound);
            Destroy(collision.gameObject);

            if (collectedCount % 5 == 0)
            {
                IncreaseSpeed();
            }
        }
        else if (collision.CompareTag("Obstacle") && canMove)
        {
            canMove = false;
            audioSource.Stop();
            audioSource.PlayOneShot(hitSound);
            StartCoroutine(GameOver());
        }
    }

    void IncreaseSpeed()
    {
        speed *= Random.Range(1.1f, 1.2f);
    }

    IEnumerator GameOver()
{
    yield return new WaitForSeconds(3f);
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
