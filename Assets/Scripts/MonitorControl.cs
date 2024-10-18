using UnityEngine;

public class MonitorControl : MonoBehaviour
{
    [SerializeField] private Sprite spriteBroken;
    [SerializeField] private GameObject goChannel;
    [SerializeField] private GameObject goExplosion;
    //private SpriteRenderer spInitial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spInitial = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            PlayerMovement.Instance.AddForceOnImpact(10f);
            StartExplosion();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = spriteBroken;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            goChannel.SetActive(false);
        }
    }

    public void StartExplosion()
    {
        goExplosion.GetComponent<SpriteRenderer>().enabled = true;
        goExplosion.GetComponent<Animator>().enabled = true;
    }
}
