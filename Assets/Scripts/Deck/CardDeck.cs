using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CardDeck <T>
{
    public event Action OnEmptyDeck = delegate { }; // when the card count hits 0

    private List<T> _cards = new List<T>();
    public List<T> Cards
    {
        get { return _cards; }
        set { _cards = value; }
    }

    public int Count
    {
        get { return Cards.Count; }
    }

    public CardDeck(List<T> cards)
    {
        this._cards = cards;
    }

    public int LastIndex
    {
        get
        {
            if (_cards.Count == 0)
            {
                return 0;
            }
            else
            {
                return _cards.Count - 1;
            }
        }
    }

    private int GetIndexFromPosition(DeckPosition position)
    {
        int newPositionIndex = 0;
        // if deck is empty, index should always be 0
        if (_cards.Count == 0)
        {
            newPositionIndex = 0;
        }
        //get end infex if it's on 'from the top'
        if (position == DeckPosition.Top)
        {
            newPositionIndex = LastIndex;
        }

        //randomize if drawing from the middle
        else if (position == DeckPosition.Middle)
        {
            newPositionIndex = UnityEngine.Random.Range(0, LastIndex);
        }

        // get 0 index if it's from the bottom
        else if (position == DeckPosition.Bottom)
        {
            newPositionIndex = 0;
        }

        return newPositionIndex;
    }

    public void Add(List<T> cards, bool onTop = true)
    {
        if (onTop)
        {
            _cards.InsertRange(0, cards);
        }
        else
        {
            _cards.AddRange(cards);
        }
    }

    public void Add(T card, DeckPosition position = DeckPosition.Top)
    {
        //bodyguard
        if (card == null) { return; }
        int targetIndex = GetIndexFromPosition(position);
        // to add it to the 'Top' add it at the end.
        // by default Insert() moves index upward.
        if (targetIndex == LastIndex)
        {
            _cards.Add(card);
        }
        else
        {
            _cards.Insert(targetIndex, card);
        }
    }

    public void Remove(int cardIndex)
    {
        if(cardIndex < 0 || cardIndex > Cards.Count - 1) { return; } // index doesn't fall within range
        //find out which card to remove
        Cards.RemoveAt(cardIndex);
    }

    public List<T> Draw(int numberToDraw, bool onTop = true)
    {
        if(numberToDraw <= 0) { return new List<T>(); } // drawing 0 cards doesn't work

        // if we're asked to draw too many, only draw what we can
        if(numberToDraw > _cards.Count)
        {
            numberToDraw = _cards.Count;
        }
        // draw the cards we're able to
        List<T> drawnCards = new List<T>();
        for (int i = 0; i < numberToDraw; i++)
        {
            drawnCards.Add(_cards[0]);
            _cards.RemoveAt(0);
        }
        //check if we've ran out of cards
        if(Cards.Count == 0)
        {
            OnEmptyDeck.Invoke();
        }
        return drawnCards;
    }

    public void Shuffle()
    {
        for (int i = _cards.Count -1; i>0; --i)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T card = _cards[j];
            _cards[j] = _cards[i];
            _cards[i] = card;
        }
    }
}
