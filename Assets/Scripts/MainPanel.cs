using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    public Text LabelCaesar, LabelVigenere;
    public Slider CaesarOffset;
    public InputField VigenereKeyword, PlainText, CipherText;
    public Color ColorActive, ColorDisabled;

    const int asciiOffset = 65;
    const int asciiLimit = 90;
    const int lowerCaseOffset = 97;
    const int lowerCaseConvert = 32;
    const int abcSize = 26;

    bool _caesar;

    void Start()
    {
        _caesar = true;
    }

    void Update()
    {
        CaesarOffset.gameObject.SetActive(_caesar);
        VigenereKeyword.gameObject.SetActive(!_caesar);

        LabelCaesar.color = _caesar ? ColorActive : ColorDisabled;       
        LabelVigenere.color = !_caesar ? ColorActive : ColorDisabled;
        LabelCaesar.fontStyle = _caesar ? FontStyle.Bold : FontStyle.Normal;
        LabelVigenere.fontStyle = !_caesar ? FontStyle.Bold : FontStyle.Normal;
    }

    public void OnCipherChanged(float newVal)
    {
        _caesar = (newVal == 0f);
        CipherText.text = "";
        PlainText.text = "";
        VigenereKeyword.text = "";
    }

    public void OnModeChanged(float newVal)
    {
        OnPlainTextChanged(PlainText.text);
    }

    public void OnCaesarOffsetChanged(float newVal)
    {
        OnPlainTextChanged(PlainText.text);
    }

    public void OnVigenereKeywordChanged(string newVal)
    {
        newVal = StripString(newVal);
        if (newVal.Length > 0)
        {         
            VigenereKeyword.text = newVal.ToUpper();
            OnPlainTextChanged(PlainText.text);
        } 
    }

    public void OnPlainTextChanged(string newVal)
    {
        newVal = StripString(newVal);

        if (_caesar)
        {
            CipherText.text = OnCaesarEncode((int)CaesarOffset.value, newVal);
        }
        else
        {
            CipherText.text = OnVigenereEncode(VigenereKeyword.text, newVal);
        }
    }

    public void OnCipherTextChanged(string newVal)
    {
        newVal = StripString(newVal);

        if (_caesar)
        {
            PlainText.text = OnCaesarDecode((int)CaesarOffset.value, newVal);
        }
        else
        {
            PlainText.text = OnVigenereDecode(VigenereKeyword.text, newVal);
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

        if (kLength > 0)
        {
            keyword = keyword.ToUpper();

            for (int i = 0; i < msg.Length; i++)
            {
                int shift = char.ConvertToUtf32(keyword, i % kLength) - asciiOffset;
                ret += OnCaesarEncode(shift, "" + msg[i]);
            }
        }

        return ret;
    }

    public string OnVigenereDecode(string keyword, string msg)
    {
        string ret = "";
        int kLength = keyword.Length;

        if (kLength > 0)
        {
            keyword = keyword.ToUpper();

            for (int i = 0; i < msg.Length; i++)
            {
                int shift = char.ConvertToUtf32(keyword, i % kLength) - asciiOffset;
                ret += OnCaesarDecode(shift, "" + msg[i]);
            }
        }


        return ret;
    }

    public string StripString(string msg)
    {
        msg = msg.Trim();
        string[] pieces = msg.Split(' ');

        string result = "";

        for (int i = 0; i < pieces.Length; i++)
        {
            result += pieces[i];
        }

        return result;
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

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
