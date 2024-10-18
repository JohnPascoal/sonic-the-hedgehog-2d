using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;// { get; private set; }
    [SerializeField] private Text txtRingCount;
    [SerializeField] private Text txtRingText;
    [SerializeField] private Text txtMinute, txtSecond;
    private bool gameOver;
    private int ringCount=0;
    private int minute;
    private int secondInt;
    private float second;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        txtRingText.color = ringCount > 0 ? new Color(0.9611634f, 1, 0.0518868f) : new Color(0.8490566f, 0.1108455f, 0.02002492f);

        if (!gameOver)
        {
            second += Time.deltaTime;
            secondInt = (int)second;
            txtSecond.text = secondInt.ToString();

            if (second >= 60)
            {
                second = 0;
                minute++;
                txtMinute.text = minute.ToString();
            }

        }
    }

    public void SetRingQuantity()
    {
        ringCount++;
        txtRingCount.text = ringCount.ToString();
    }
}
