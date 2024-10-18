using UnityEngine;

public class MotobugControl : MonoBehaviour
{
    private Rigidbody2D rgb;
    [SerializeField] private float moveSpeed;
    int direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        var direction = transform.rotation.y == 0 ? -1 : 1;

        rgb.linearVelocity = new Vector2(moveSpeed * Time.deltaTime * direction, 0f);
    }
}
