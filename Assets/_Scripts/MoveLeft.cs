using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float destroyX = -10f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}