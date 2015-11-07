using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ValueField : MonoBehaviour
{
    public int Value;
    Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    public void SetValue(float newVal)
    {
        Value = (int)newVal;
        _text.text = Value.ToString();
    }
}
