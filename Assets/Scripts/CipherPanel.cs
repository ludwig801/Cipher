using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class CipherPanel : MonoBehaviour
{
    public Text LabelCaesar, LabelVigenere;
    public Text LabeEncode, LabelDecode;
    public GameObject OffsetCaesar, OffsetVigenere;
    public Slider CaesarOffset;
    public InputField VigenereKeyword, MessageText, CipherText;
    public Color ColorActive, ColorDisabled;

    const int asciiOffset = 65;
    const int asciiLimit = 90;
    const int lowerCaseOffset = 97;
    const int lowerCaseConvert = 32;
    const int abcSize = 26;

    bool _caesar, _encode;

    void Start()
    {
        _caesar = true;
        _encode = true;
    }

    void Update()
    {
        OffsetCaesar.SetActive(_caesar);
        OffsetVigenere.SetActive(!_caesar);

        LabelCaesar.color = _caesar ? ColorActive : ColorDisabled;       
        LabelVigenere.color = !_caesar ? ColorActive : ColorDisabled;      
        LabeEncode.color = _encode ? ColorActive : ColorDisabled;
        LabelDecode.color = !_encode ? ColorActive : ColorDisabled;

        LabelCaesar.fontStyle = _caesar ? FontStyle.Bold : FontStyle.Normal;
        LabelVigenere.fontStyle = !_caesar ? FontStyle.Bold : FontStyle.Normal;
        LabeEncode.fontStyle = _encode ? FontStyle.Bold : FontStyle.Normal;
        LabelDecode.fontStyle = !_encode ? FontStyle.Bold : FontStyle.Normal;
    }

    public void OnCipherChanged(float newVal)
    {
        _caesar = (newVal == 0f);
        CipherText.text = "";
        MessageText.text = "";
        VigenereKeyword.text = "";
    }

    public void OnModeChanged(float newVal)
    {
        _encode = (newVal == 0f);
        OnInputChanged(MessageText.text.Trim());
    }

    public void OnCaesarOffsetChanged(float newVal)
    {
        OnInputChanged(MessageText.text.Trim());
    }

    public void OnVigenereKeywordChanged(string newVal)
    {
        if (newVal.Length > 0)
        {
            OnInputChanged(MessageText.text.Trim());
        } 
    }

    public void OnInputChanged(string newVal)
    {
        if (_caesar)
        {
            if (_encode)
            {
                CipherText.text = OnCaesarEncode((int)CaesarOffset.value, newVal.Trim());
            }
            else
            {
                CipherText.text = OnCaesarDecode((int)CaesarOffset.value, newVal.Trim());
            }
        }
        else
        {
            if (_encode)
            {
                CipherText.text = OnVigenereEncode(VigenereKeyword.text, newVal.Trim());
            }
            else
            {
                CipherText.text = OnVigenereDecode(VigenereKeyword.text, newVal.Trim());
            }
        }
    }

    public string OnCaesarEncode(int shift, string msg)
    {
        string ret = "";

        for (int i = 0; i < msg.Length; i++)
        {
            int ascii = ToUpperCase(msg[i]);

            if (ascii < asciiOffset || ascii > asciiLimit)
            {
                continue;
            }

            ascii -= asciiOffset;
            ascii += shift;
            ascii %= abcSize;
            ascii += asciiOffset;
            ret += char.ConvertFromUtf32(ascii);
        }

        return ret;
    }

    public string OnCaesarDecode(int shift, string msg)
    {
        string ret = "";

        for (int i = 0; i < msg.Length; i++)
        {
            int ascii = ToUpperCase(msg[i]);

            if (!Alphanumeric(ascii))
            {
                continue;
            }

            ascii -= asciiOffset;
            ascii -= shift;
            ascii %= abcSize;
            if (ascii < 0)
            {
                ascii += abcSize;
            }
            ascii += asciiOffset;

            ret += char.ConvertFromUtf32(ascii);
        }

        return ret;
    }

    public string OnVigenereEncode(string keyword, string msg)
    {
        string ret = "";
        int kLength = keyword.Length;

        keyword = keyword.ToUpper();

        for (int i = 0; i < msg.Length; i++)
        {
            int shift = char.ConvertToUtf32(keyword, i % kLength) - asciiOffset;
            ret += OnCaesarEncode(shift, "" + msg[i]);
        }

        return ret;
    }

    public string OnVigenereDecode(string keyword, string msg)
    {
        string ret = "";
        int kLength = keyword.Length;

        keyword = keyword.ToUpper();

        for (int i = 0; i < msg.Length; i++)
        {
            int shift = char.ConvertToUtf32(keyword, i % kLength) - asciiOffset;
            ret += OnCaesarDecode(shift, "" + msg[i]);
        }

        return ret;
    }

    public int ToUpperCase(int ascii)
    {
        if (ascii >= lowerCaseOffset)
        {
            ascii -= lowerCaseConvert;
        }

        return ascii;
    }

    public bool Alphanumeric(int ascii)
    {
        return (ascii >= asciiOffset && ascii <= asciiLimit);
    }
}
