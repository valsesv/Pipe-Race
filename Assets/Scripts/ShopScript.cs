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
    public List<GameObject> columns = new List<GameObject>();
    private Vector3[] columnsPos = new Vector3[4];
    public GameObject pipe, PipessSmoke;
    public List<Text> texts = new List<Text>();
    private int[] prices = new int[12];

    private void Awake()
    {
        texts[4].text = "" + PlayerPrefs.GetInt("WITgameO");
        num = PlayerPrefs.GetInt("CNNumGameO");
        Select();
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
        for (int i = 0; i < 4; i++)
        {
            columnsPos[i].y = 960;
            columnsPos[i].z = 0;
        }
        LeftColumn = 0;
        columnsPos[0].x = 247.5495f;
        columnsPos[1].x = 540;
        columnsPos[2].x = 832.4505f;
        columnsPos[3].x = 5000;


        if (PlayerPrefs.HasKey("IfAdsOn"))
            AdButton.SetActive(false);
    }

    public void SelecButton()
    {
        if (PlayerPrefs.GetInt("HaveSkin" + num) == 1)
            if (PlayerPrefs.GetInt("CNNumGameO") == num)
                texts[0].text = "Selected";
            else
                texts[0].text = "Select";
        else
            texts[0].text = "" + prices[num];
    }

    public void skin(int b)
    {
        num = b;
        pipe.GetComponent<MeshRenderer>().material = parents[num];
        //PipessSmoke.GetComponent<ParticleSystem>().startColor = new Color(parents[num].color.r, parents[num].color.g, parents[num].color.b, 0.235f);
        SelecButton();
    }

    public void Select()
    {
        if (PlayerPrefs.GetInt("HaveSkin" + num) == 1)
        {
            PlayerPrefs.SetInt("CNNumGameO", num);
            //SelectedColor.transform.position = parents[num].transform.position;
            //SelectedColor.transform.SetParent(parents[num].transform);

        }
        else if (PlayerPrefs.GetInt("WITgameO") >= prices[num])
            BuyPanel.SetActive(true);
        else
        {
            texts[5].text = "Where's coins?\nYou can earn or buy them in shop.";
            SorryPanel.SetActive(true);
        }
        SelecButton();
    }

    public void HideSorryMenu()
    {
        SorryPanel.SetActive(false);
    }

    public void HideBuyMenu()
    {
        BuyPanel.SetActive(false);
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
        Select();
        parentsScin[num].color = Color.white;
    }
    public void ShopOff()
    {
        num = PlayerPrefs.GetInt("CNNumGameO");
        pipe.GetComponent<MeshRenderer>().material = parents[num];
    }

    public void SavePos()
    {
        if (LeftColumn == 0)
            for (int i = 0; i < 4; i++)
            {
                columnsPos[i] = columns[i].transform.position;
            }
    }

    public void SwapPageLeft()
    {
        columns[LeftColumn].transform.position = columnsPos[3];
        for (int i = 1; i < columns.Count; i++)
            columns[(LeftColumn + i) % columns.Count].transform.position = columnsPos[i - 1];

        LeftColumn++;
        if (LeftColumn >= columns.Count)
            LeftColumn = 0;
    }

    public void SwapPageRigth()
    {
        for (int i = 0; i < 4; i++)
            columns[(LeftColumn + i) % columns.Count].transform.position = columnsPos[(i + 1) % 4];

        LeftColumn--;
        if (LeftColumn <= 0)
            LeftColumn = columns.Count - 1;
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
