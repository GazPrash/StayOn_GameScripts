using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandlePlayerEmission : MonoBehaviour
{
    [System.NonSerialized] public Material PlayerMaterialBody, PlayerMaterialBasics, PlayerMaterialHelm;
    [SerializeField] public float EmissionIntensity;

    void Start()
    {
        var Player = GameObject.FindGameObjectWithTag("Player");
        var AllMaterials = this.transform.Find("Player").GetComponent<Renderer>().materials;
        PlayerMaterialBasics = AllMaterials[0];
        PlayerMaterialBody = AllMaterials[1];
        PlayerMaterialHelm = AllMaterials[2];
        TurnOnEmission();
    }

    void TurnOnEmission(){
        PlayerMaterialBody.EnableKeyword("_EMISSION");
        PlayerMaterialBody.SetColor("_EmissionColor", Color.white * EmissionIntensity);

        PlayerMaterialBasics.EnableKeyword("_EMISSION");
        Color HelmColor;
        var hexcolor = "#CC3300";
        ColorUtility.TryParseHtmlString(hexcolor, out HelmColor);
        PlayerMaterialBasics.SetColor("_EmissionColor", HelmColor * EmissionIntensity);

        PlayerMaterialHelm.EnableKeyword("_EMISSION");
        PlayerMaterialHelm.SetColor("_EmissionColor", HelmColor * EmissionIntensity);

    }

    void TurnOffEmission(){
        PlayerMaterialBody.DisableKeyword("_EMISSION");
        PlayerMaterialBasics.DisableKeyword("_EMISSION");
        PlayerMaterialHelm.DisableKeyword("_EMISSION");
    }

    void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Deathrun")){
            TurnOffEmission();
        }
    }
}
