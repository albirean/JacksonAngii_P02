using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CardDeckSystem<T>
{
    public CardDeck<T> DrawPile { get; private set; }
    public CardDeck<T> Hand { get; private set; }
    public CardDeck<T> DiscardPile { get; private set; }

    public event Action<int> OnDrawPileOverdraw = delegate { };
    public event Action<int> OnDiscardPileOverdraw = delegate { };

    public CardDeckSystem(List<T> cards)
    {
        DrawPile = new CardDeck<T>(cards);
        //hand and discard need to start off with empty decks
        List<T> emptyDeck = new List<T>();
        Hand = new CardDeck<T>(emptyDeck);
        DiscardPile = new CardDeck<T>(emptyDeck);
    }

    public void Draw(int numberToDraw = 1)
    {
        // drawing 0 cards doesn't make sense
        if(numberToDraw <= 0) { return; }
        //if we're able to draw multiple, do it
        if(DrawPile.Count >= numberToDraw)
        {
            List<T> drawnCards = DrawPile.Draw(numberToDraw);
            Hand.Add(drawnCards);
        }

        // not enough cards! Trigger overdraw event.
        else
        {
            //track the number of remaining cards we will have, because we are about to alter data
            int undrawnCards = numberToDraw - DrawPile.Count;
            numberToDraw = DrawPile.Count;
            //draw the cards
            List<T> drawnCards = DrawPile.Draw(numberToDraw);
            //send out notif on remaining cards
            OnDrawPileOverdraw.Invoke(undrawnCards);
        }
    }

    public void DrawFromDiscard(int numberToDraw = 1)
    {
        if (numberToDraw == 0) { return; } // drawing 0 cards doesn't make sense
        // since we're drawing from discard, typically wanna draw from bottom
        List<T> drawnCards = DiscardPile.Draw(numberToDraw, false);
        Hand.Add(drawnCards, false);
        //account for running out of cards to draw
        if (numberToDraw > drawnCards.Count)
        {
            int undrawnCards = numberToDraw - drawnCards.Count;
            OnDiscardPileOverdraw.Invoke(undrawnCards);
        }
    }

    public void Discard(int cardIndex)
    {
        if(cardIndex < 0 || cardIndex > Hand.Cards.Count - 1) { return; } // index doesn't fall within range
        // find out which card to remove
        T cardToDiscard = Hand.Cards[cardIndex];
        //pass it through
        Hand.Remove(cardIndex);
        DiscardPile.Add(cardToDiscard); // add to bottom.
    }

    public void Discard(List<int> cardIndexes)
    {
        if(cardIndexes.Count > Hand.Cards.Count) { return; } // can't discard more than we have!

        // create the list of cards to Remove
        List<T> cardsToDiscard = new List<T>();
        //grab refs to cards we want to discard
        foreach(int cardIndex in cardIndexes)
        {
            // validate the index
            if(cardIndex < 0 || cardIndex > Hand.Cards.Count)
            {
                continue;
            }
            cardsToDiscard.Add(Hand.Cards[cardIndex]);
        }
        //discard them
        foreach(T card in cardsToDiscard)
        {
            Hand.Cards.Remove(card);
            //discard piles are typically face up, meaning we want to add to the bottom
            DiscardPile.Add(card);
        }
        //cleanup
        cardsToDiscard.Clear();
    }

    public void DiscardRandom()
    {
        //TODO
    }

    public void DiscardHand()
    {
        for (int i = 0; i < Hand.Count; i++)
        {
            Discard(i);
        }
    }

    public void RemoveFromHand(List<int> cardIndexes)
    {
        //TODO
    }

    public void ShuffleDiscardIntoDrawPile()
    {
        foreach (T cardToShuffle in DiscardPile.Cards)
        {
            DrawPile.Add(cardToShuffle);
        }
        DiscardPile.Cards.Clear();
        DiscardPile.Shuffle();
    }
}
