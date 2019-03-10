using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour
{
    private UnityEngine.UI.Text materialsText;
    private UnityEngine.UI.Text artisanHPText;
    private UnityEngine.UI.Text bardHPText;
    private UnityEngine.UI.Text thiefHPText;
    private UnityEngine.UI.Text warriorHPText;
    private GameManager model;
    public GameObject artisan;
    public GameObject bard;
    public GameObject thief;
    public GameObject warrior;

    // Start is called before the first frame update
    void Start()
    {
        materialsText = GameObject.Find("MaterialsText").GetComponent<UnityEngine.UI.Text>();
        artisanHPText = GameObject.Find("ArtisanHPText").GetComponent<UnityEngine.UI.Text>();
        bardHPText = GameObject.Find("BardHPText").GetComponent<UnityEngine.UI.Text>();
        thiefHPText = GameObject.Find("ThiefHPText").GetComponent<UnityEngine.UI.Text>();
        warriorHPText = GameObject.Find("WarriorHPText").GetComponent<UnityEngine.UI.Text>();
        model = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        materialsText.text = "Materials: " + model.getMaterials();

        artisanHPText.text = "Artisan HP: " + artisan.GetComponent<HealthController>().GetCurrentHealth();
        bardHPText.text = "Bard HP: " + bard.GetComponent<HealthController>().GetCurrentHealth();
        thiefHPText.text = "Thief HP: " + thief.GetComponent<HealthController>().GetCurrentHealth();
        warriorHPText.text = "Warrior HP: " + warrior.GetComponent<HealthController>().GetCurrentHealth();
    }
}
