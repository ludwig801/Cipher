using UnityEngine.UI;

public class ValueField : UnityEngine.MonoBehaviour
{
    Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    public void SetValue(float newVal)
    {
        _text.text = ((int)newVal).ToString();
    }
}
