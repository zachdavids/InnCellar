using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour
{
    #region Attributes

    [SerializeField] private GameObject _artisan;
    [SerializeField] private GameObject _bard;
    [SerializeField] private GameObject _thief;
    [SerializeField] private GameObject _warrior;

    private UnityEngine.UI.Text _artisanHPText;
    private UnityEngine.UI.Text _bardHPText;
    private UnityEngine.UI.Text _thiefHPText;
    private UnityEngine.UI.Text _warriorHPText;

    #endregion

    #region Monobehaviour Functions

    // Start is called before the first frame update
    void Start()
    {
        _artisanHPText = GameObject.Find("ArtisanHPText").GetComponent<UnityEngine.UI.Text>();
        _bardHPText = GameObject.Find("BardHPText").GetComponent<UnityEngine.UI.Text>();
        _thiefHPText = GameObject.Find("ThiefHPText").GetComponent<UnityEngine.UI.Text>();
        _warriorHPText = GameObject.Find("WarriorHPText").GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _artisanHPText.text = "Artisan HP: " + _artisan.GetComponent<HealthController>().currentHealth;
        _bardHPText.text = "Bard HP: " + _bard.GetComponent<HealthController>().currentHealth;
        _thiefHPText.text = "Thief HP: " + _thief.GetComponent<HealthController>().currentHealth;
        _warriorHPText.text = "Warrior HP: " + _warrior.GetComponent<HealthController>().currentHealth;
    }

    #endregion
}
