using UnityEngine;

public class RingControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Attack"))
        {
            gameObject.SetActive(false);
            LevelManager.Instance.SetRingQuantity();
        }
    }
}
