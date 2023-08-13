using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    private int num, LeftColumn;
    public GameObject SelectedColor, BuyPanel, SorryPanel, panel, ShopPanel, CoinShop, BuyCoin, AdButton;
    public List<Material> parents = new List<Material>();
    public List<Image> parentsScin = new List<Image>();
    public GameObject pipe, PipessSmoke;
    public List<Text> texts = new List<Text>();
    private int[] prices = new int[12];

    private void Awake()
    {
        texts[4].text = "" + PlayerPrefs.GetInt("WITgameO");
        num = PlayerPrefs.GetInt("CNNumGameO");
        
        int c = 0;
        for (int i = 1; i < parents.Count; i++)
        {
            if (!PlayerPrefs.HasKey("HaveSkin" + i))
                PlayerPrefs.SetInt("HaveSkin" + i, 0);
            if (PlayerPrefs.GetInt("HaveSkin" + i) == 1)
                c++;
            else
            {
                parentsScin[i].color = new Color(94 / 255f, 94 / 255f, 94 / 255f);
            }
        }

        for (int i = 1; i < parents.Count; i++)
        {
            prices[i] = 50 + 25 * c;
            if (prices[i] > 300)
                prices[i] = 300;
        }


        if (PlayerPrefs.HasKey("IfAdsOn"))
            AdButton.SetActive(false);
    }


    public void BuySkin()
    {
        PlayerPrefs.SetInt("WITgameO", PlayerPrefs.GetInt("WITgameO") - prices[num]);
        PlayerPrefs.SetInt("HaveSkin" + num, 1);
        BuyPanel.SetActive(false);
        texts[4].text = "" + PlayerPrefs.GetInt("WITgameO");
        int c = 0;
        for (int i = 1; i < parents.Count; i++)
        {
            if (PlayerPrefs.GetInt("HaveSkin" + i) == 1)
                c++;
        }
        for (int i = 1; i < parents.Count; i++)
        {
            prices[i] = 50 + 25 * c;
            if (prices[i] > 300)
                prices[i] = 300;
        }
        parentsScin[num].color = Color.white;
    }
    public void ShopOff()
    {
        num = PlayerPrefs.GetInt("CNNumGameO");
        pipe.GetComponent<MeshRenderer>().material = parents[num];
    }

    public void CoinShopOn(){
        CoinShop.SetActive(true);
        BuyCoin.SetActive(false);
    }
    
    public void BuyCoinOn(){
        BuyCoin.SetActive(true);
        CoinShop.SetActive(false);
    }


}
