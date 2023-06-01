using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Items")] //controla UI dos itens
    [SerializeField] private Image waterUIBar;
    [SerializeField] private TextMeshProUGUI uiTextWater;
    [SerializeField] private TextMeshProUGUI uiTextWood;
    [SerializeField] private TextMeshProUGUI uiTextFish;
    [SerializeField] private TextMeshProUGUI uiTextCarrot;

    //mesmo método acima porém em lista, cuida da lista de ferramentas/itens selecionados
    public List<Image> toolsUI = new List<Image>();

    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private PlayerItems playerItems;
    private Player player;
    public Slider healthBar;
    public Stat hp;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        player = playerItems.GetComponent <Player>();
    }

    // Start is called before the first frame update
    public void Set(int cur, int max){
        healthBar.maxValue = max;
        healthBar.value = cur;
    }

    // Update is called once per frame
    void Update()
    {
        uiTextWater.text = playerItems.currentWater.ToString();
        uiTextWood.text = playerItems.totalWood.ToString();
        uiTextFish.text = playerItems.fishes.ToString();
        uiTextCarrot.text = playerItems.carrots.ToString();


        toolsUI[player.handlingObj].color = selectColor;

        for (int i = 0; i < toolsUI.Count; i++)
        {
            if (i == player.handlingObj)
            {
                toolsUI[i].color = selectColor;
            }
            else
            {
                toolsUI[i].color = alphaColor;
            }
        }
    }
}
