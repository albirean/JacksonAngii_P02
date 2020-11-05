using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCardView : MonoBehaviour
{
    [SerializeField] Text _nameTextUI = null;
    [SerializeField] Text _costTextUI = null;
    [SerializeField] Text _descriptionTextUI = null;
    [SerializeField] Image _graphicUI = null;

    public void Display(AbilityCard abilityCard)
    {
        _nameTextUI.text = abilityCard.Name;
        _costTextUI.text = abilityCard.Cost.ToString();
        _descriptionTextUI.text = abilityCard.Desc.ToString();
        _graphicUI.sprite = abilityCard.Graphic;
    }
}
