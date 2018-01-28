using System.Collections.Generic;
using UnityEngine;

public class LanguageLibrary : MonoBehaviour
{
    public enum WordID
    {
        BUS = 0,
        FISH,
        COMING,
        TICKET,
        APPLE,
        PEAR,
        MARKET,
        GOING,
        POLE,
        PERSON,
        BOOK,
        CONTAINER,
        HOUSE,
        BUYING,
        MATCHES,
        GEM,
        MONEY,
        HILL,
        REWARD,
        ROSE,
        NUMBER_OF_WORDS
    }


    public struct Word
    {
        Sprite symbol;
        Sprite icon;

        string meaning;

        Word(Sprite _symbol, Sprite _icon, string _meaning)
        {
            symbol = _symbol;
            icon = _icon;
            meaning = _meaning;
        }
    }

    [SerializeField]
    Sprite[] symbols;

    [SerializeField]
    Sprite[] icons;

    [SerializeField]
    string[] meanings;

    Sprite GetSymbol(WordID word_id)
    {
        return symbols[(int)word_id];
    }

    Sprite GetIcon(WordID word_id)
    {
        return icons[(int)word_id];
    }

    string GetMeaning(WordID word_id)
    {
        return meanings[(int)word_id];
    }

}
