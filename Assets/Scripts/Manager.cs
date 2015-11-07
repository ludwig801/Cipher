using UnityEngine;

public class Manager : MonoBehaviour
{
    public CaesarPanel Caesar;
    public VigenerePanel Vigenere;
    public Tooltip Tooltip;
    public GameObject TitlePanel, FooterPanel;

    public static Manager Instance;
    string copyText;

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCaesarSelected(bool value)
    {
        if (value)
        {
            Vigenere.Hide();
            TitlePanel.SetActive(false);
            FooterPanel.SetActive(false);
        }
        else
        {
            Vigenere.Show();
            TitlePanel.SetActive(true);
            FooterPanel.SetActive(true);
        }
    }

    public void OnVigenereSelected(bool value)
    {
        if (value)
        {
            Caesar.Hide();
            TitlePanel.SetActive(false);
            FooterPanel.SetActive(false);
        }
        else
        {
            Caesar.Show();
            TitlePanel.SetActive(true);
            FooterPanel.SetActive(true);
        }
    }
}
