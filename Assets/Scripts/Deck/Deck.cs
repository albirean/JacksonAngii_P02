﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Deck <T> where T : Card
{
    List<T> _cards = new List<T>();

    public event Action Emptied = delegate { };
    public event Action<T> CardAdded = delegate { };
    public event Action<T> CardRemoved = delegate { };

    public int Count => _cards.Count;
    public T TopItem => _cards[_cards.Count - 1];
    public T BottomItem => _cards[0];
    public bool IsEmpty => _cards.Count == 0;
    public int LastIndex
    {
        get
        {
            if(_cards.Count == 0)
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
        if(_cards.Count == 0)
        {
            newPositionIndex = 0;
        }
        //get end infex if it's on 'from the top'
        if(position == DeckPosition.Top)
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

    public void Add(T card, DeckPosition position = DeckPosition.Top)
    {
        //bodyguard
        if(card == null) { return; }
        int targetIndex = GetIndexFromPosition(position);
        // to add it to the 'Top' add it at the end.
        // by default Insert() moves index upward.
        if(targetIndex == LastIndex)
        {
            _cards.Add(card);
        }
        else
        {
            _cards.Insert(targetIndex, card);
        }
        CardAdded?.Invoke(card);
    }

    public void Add(List<T> cards, DeckPosition position = DeckPosition.Top)
    {
        int itemCount = cards.Count;
        for(int i = 0; i < itemCount; i++)
        {
            Add(cards[i], position);
        }
    }

    //Draws next item (top of deck)
    public T Draw(DeckPosition position = DeckPosition.Top)
    {
        if (IsEmpty)
        {
            Debug.LogWarning("Deck: Cannot draw new item - Deck is Empty!");
            return default;
        }

        int targetIndex = GetIndexFromPosition(position);

        T cardToRemove = _cards[targetIndex];
        Remove(targetIndex);

        return cardToRemove;
    }

    public void Remove(int index)
    {
        if (IsEmpty)
        {
            Debug.LogWarning("Deck: Nothing to remove; deck is already empty");
            return;
        }

        else if (!IsIndexWithinListRange(index))
        {
            Debug.LogWarning("Deck: Nothing to remove; index out of range");
            return;
        }

        T removedItem = _cards[index];
        _cards.RemoveAt(index);

        CardRemoved?.Invoke(removedItem);

        if(_cards.Count == 0)
        {
            Emptied?.Invoke();
        }
    }

    bool IsIndexWithinListRange(int index)
    {
        // if index is within the range of contents
        if (index >= 0 && index <= _cards.Count - 1)
        {
            return true;
        }

        Debug.LogWarning("Deck: index outside of range;" + " index: " + index);

        return false;
    }

    public T GetCard(int index)
    {
        if(_cards[index] != null)
        {
            return _cards[index];
        }
        else
        {
            return default;
        }
    }

    public void Shuffle()
    {
        // start at the top, randomly swapping cards as we move our way down
        for(int currentIndex = Count - 1; currentIndex > 0; --currentIndex)
        {
            // choose a random card, but not ones we've already placed
            int randomIndex = UnityEngine.Random.Range(0, currentIndex + 1);
            T randomCard = _cards[randomIndex];
            // random card swaps place with out current index
            _cards[randomIndex] = _cards[currentIndex];
            _cards[currentIndex] = randomCard;

            //move downwards to next card index
        }
    }


}
