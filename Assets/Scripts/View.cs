using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private Text scoreUI;

    [SerializeField] private HorizontalLayoutGroup livesUI;
    [SerializeField] private Sprite livesOn;
    [SerializeField] private Sprite livesOff;

    [SerializeField] private VerticalLayoutGroup inventoryUI;

    private Orchestra orchestra;

    private void Start()
    {
        orchestra = GameObject.Find("Manager").GetComponent<Orchestra>();

        // Score
        scoreUI.text = "0";

        // Lives
        for (int i = 0; i < livesUI.GetComponentsInChildren<Image>().Length; i++)
        {
            livesUI.GetComponentsInChildren<Image>()[i].sprite = livesOn;
        }
    }

    public void UpdateScore(int score)
    {
        scoreUI.text = score.ToString();
    }

    public void UpdateLives(int lives)  // NOTE: currently, you can only lose lives
    {
        Image[] images = livesUI.GetComponentsInChildren<Image>();
        images[lives].sprite = livesOff; 
    }

    public void UpdateInventory()
    {
        for (int i = inventoryUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(inventoryUI.transform.GetChild(i).gameObject);
        }

        foreach (InstrumentWrapper i in orchestra.instruments)
        {
            if(i.instrument != Instrument.Instruments.MAIN && i.tilemap.activeInHierarchy)
            {
                Instantiate(i.inventory, inventoryUI.transform);
            }
        }
    }
}
