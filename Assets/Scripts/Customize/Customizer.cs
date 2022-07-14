using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour
{
    public Material tile;
    public Material trap;
    public Material collectible;

    public MeshFilter playerMesh;

    ColorTheme defaultTheme;

    public static Customizer instance;

    [HideInInspector] public ShopModel currentModel;


    public List<ShopTheme> allThemes = new List<ShopTheme>();
    public List<ShopModel> allModels = new List<ShopModel>();

    private void Awake()
    {
        instance = this;
    }

    
    void Start()
    {
#if UNITY_EDITOR
        defaultTheme = new ColorTheme(tile.GetColor("_Color"), trap.GetColor("_Color"), collectible.GetColor("_Color"), collectible.GetColor("_EmissionColor"));
#endif

        LoadBoughtThemes();

        ChangeTheme(allThemes[PlayerSave.instance.currentTheme].theme);
        ChangeModel(allModels[PlayerSave.instance.currentModel].mesh);
    }

    public void ChangeTheme(ColorTheme theme)
    {
        tile.SetColor("_Color", theme.tileColor);
        trap.SetColor("_Color", theme.trapColor);
        collectible.SetColor("_Color", theme.collectibleColor);
        collectible.SetColor("_EmissionColor", theme.collectibleEmissiveColor);
    }

    public void ChangeModel(Mesh mesh)
    {
        playerMesh.mesh = mesh;
    }

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        ChangeTheme(defaultTheme);
#endif
    }

    public void LoadBoughtThemes()
    {
        for (int i = 0; i < allThemes.Count; i++)
        {
            if (PlayerSave.instance.unlockedThemes[i] == '1') allThemes[i].BoughtState(true);
        }
    }
}

[System.Serializable]
public class ColorTheme
{
    public Color tileColor;
    public Color trapColor;
    public Color collectibleColor;
    public Color collectibleEmissiveColor;

    public ColorTheme(Color tileColor, Color trapColor, Color collectibleColor, Color collectibleEmissiveColor)
    {
        this.tileColor = tileColor;
        this.trapColor = trapColor;
        this.collectibleColor = collectibleColor;
        this.collectibleEmissiveColor = collectibleEmissiveColor;
    }
}
