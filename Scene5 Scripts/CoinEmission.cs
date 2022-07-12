using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinEmission : MonoBehaviour
{
    [SerializeField] public float EmissionIntensity;
    [System.NonSerialized]public Material CoinMaterial;

    public bool CoinEmissionOn = false;
    
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Deathrun"){
            CoinMaterial = this.GetComponent<Renderer>().sharedMaterial;
            TurnOnEmission();
        }
    }

    public void TurnOnEmission(){
        CoinEmissionOn = !CoinEmissionOn;
        CoinMaterial.EnableKeyword("_EMISSION");
        Color GoldColor;
        string hexcolor = "#FFF10D";
        ColorUtility.TryParseHtmlString(hexcolor, out GoldColor);
        CoinMaterial.SetColor("_EmissionColor", GoldColor * EmissionIntensity);
    }

    void Update()
    {
        
    }
}
