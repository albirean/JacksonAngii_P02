using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckTester : MonoBehaviour
{
    [SerializeField]
    List<AbilityCardData> _abilityDeckConfig
        = new List<AbilityCardData>();
    [SerializeField] AbilityCardView _abilityCardViewFirst = null;
    [SerializeField] AbilityCardView _abilityCardView = null;
    [SerializeField] AbilityCardView _abilityCardViewLast = null;
    Deck<AbilityCard> _abilityDeck = new Deck<AbilityCard>();
    Deck<AbilityCard> _abilityDiscard = new Deck<AbilityCard>();

    [SerializeField] AbilityCardView _discardView = null;
    [SerializeField] AbilityCardView _upcomingView = null;

    Deck<AbilityCard> _playerHand = new Deck<AbilityCard>();

    private bool _hasDrawn = false;

    [SerializeField] Button _button1 = null;
    [SerializeField] Button _button2 = null;
    [SerializeField] Button _button3 = null;

    [SerializeField] Button _drawButton = null;
    [SerializeField] Button _shuffleButton = null;

    [SerializeField] GameObject _playerCardArea = null;
    [SerializeField] Material m_PlayerCard = null;
    [SerializeField] Material m_Card = null;


    private void Start()
    {
        SetupAbilityDeck();
        HidePlayerHand();
        _upcomingView.Display(_abilityDeck.TopItem);
        _button1.onClick.AddListener(TaskOnClick);
        _button2.onClick.AddListener(TaskOnClick);
        _button3.onClick.AddListener(TaskOnClick);
        _drawButton.onClick.AddListener(() => DrawCard());
        _shuffleButton.onClick.AddListener(() => ShuffleDeck());
    }

    private void HidePlayerHand()
    {
        _abilityCardViewFirst.gameObject.SetActive(false);
        _abilityCardView.gameObject.SetActive(false);
        _abilityCardViewLast.gameObject.SetActive(false);
    }

    private void ShowPlayerHand()
    {
        _abilityCardViewFirst.gameObject.SetActive(true);
        _abilityCardView.gameObject.SetActive(true);
        _abilityCardViewLast.gameObject.SetActive(true);
    }

    private void SetupAbilityDeck()
    {
        foreach(AbilityCardData abilityData in _abilityDeckConfig)
        {
            AbilityCard newAbilityCard = new AbilityCard(abilityData);
            _abilityDeck.Add(newAbilityCard);
        }

        _abilityDeck.Shuffle();
    }

    private void Update()
    {
        if(_abilityDeck.Count > 0)
        {
            _upcomingView.gameObject.SetActive(true);
            _shuffleButton.gameObject.SetActive(false);
        }

        if(_abilityDeck.Count == 0)
        {
            _shuffleButton.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PrintPlayerHand();
        }
        
    }

    void TaskOnClick()
    {
        PlayTopCard();
        Debug.Log("You have clicked the button");
    }

    void DrawCard()
    {
        Draw();
    }

    void ShuffleDeck()
    {
        if (_abilityDeck.IsEmpty)
        {
            for (int j = _abilityDiscard.Count -1; j > -1; --j)
            {
                AbilityCard cardToShuffle = _abilityDiscard.GetCard(j);
                _abilityDiscard.Remove(j);
                _abilityDeck.Add(cardToShuffle);
            }

                _abilityDeck.Shuffle();
                Debug.Log("Deck Shuffled. Deck Count: " + _abilityDeck.Count);
                Debug.Log("Discard Count: " + _abilityDiscard.Count);
            
        }
    }

    private void Draw()
    {
        if (_abilityDeck.IsEmpty)
        {
            Debug.Log("Cannot draw! Deck is Empty!");
        }

        if (!_abilityDeck.IsEmpty)
        {
            
            if (_hasDrawn == false)
            {
                Renderer CardPlayed = _playerCardArea.GetComponent<Renderer>();
                CardPlayed.material = m_Card;

                AbilityCard newCard = _abilityDeck.Draw(DeckPosition.Top);
                Debug.Log("Drew card: " + newCard.Name);
                AbilityCard firstCard = _abilityDeck.Draw(DeckPosition.Top);
                Debug.Log("Drew card: " + firstCard.Name);
                AbilityCard lastCard = _abilityDeck.Draw(DeckPosition.Top);
                Debug.Log("Drew card: " + lastCard.Name);



                _playerHand.Add(newCard, DeckPosition.Top);
                _playerHand.Add(firstCard, DeckPosition.Top);
                _playerHand.Add(lastCard, DeckPosition.Top);

                ShowPlayerHand();

                _abilityCardView.Display(newCard);
                _abilityCardViewFirst.Display(firstCard);
                _abilityCardViewLast.Display(lastCard);

                if(_abilityDeck.Count > 0)
                {
                    AbilityCard upcomingCard = _abilityDeck.TopItem;
                    _upcomingView.Display(upcomingCard);
                }
                
                if(_abilityDeck.Count == 0)
                {
                    _upcomingView.gameObject.SetActive(false);
                }

                _hasDrawn = true;
                Debug.Log("Deck Count: " + _abilityDeck.Count);
            }

            if (_hasDrawn == true)
            {
                Debug.Log("Card already drawn for the turn.");
            }
        }
        
    }

    private void PrintPlayerHand()
    {
        for(int i = 0; i<_playerHand.Count; i++)
        {
            Debug.Log("Player Hand Card: " + _playerHand.GetCard(i).Name);
        }
    }

    void PlayTopCard()
    {
        AbilityCard targetCard = _playerHand.TopItem;
        AbilityCard secondCard = _playerHand.SecondItem;
        AbilityCard thirdCard = _playerHand.ThirdItem;
        targetCard.Play();
        Renderer CardPlayed = _playerCardArea.GetComponent<Renderer>();
        CardPlayed.material = m_PlayerCard;
        //consider expanding remove to accept a deck position
        _playerHand.Remove(_playerHand.LastIndex);
        _playerHand.Remove(_playerHand.LastIndex);
        _playerHand.Remove(_playerHand.LastIndex);

        HidePlayerHand();

        _abilityDiscard.Add(targetCard);
        Debug.Log("Card added to discard: " + targetCard.Name);
        _discardView.Display(targetCard);

        _abilityDiscard.Add(secondCard);
        Debug.Log("Card added to discard: " + secondCard.Name);
        _abilityDiscard.Add(thirdCard);
        Debug.Log("Card added to discard: " + thirdCard.Name);

        Debug.Log("Discard Count: " + _abilityDiscard.Count);
        _hasDrawn = false;
    }
    
}
