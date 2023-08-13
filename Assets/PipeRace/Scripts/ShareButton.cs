using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShareButton : MonoBehaviour
{

    public void InstVseslav()
    {
        Application.OpenURL("https://www.instagram.com/vse_slav/");
    }
    public void InstDaniil()
    {
        Application.OpenURL("https://www.instagram.com/daniildemidovich/");
    }

    public void ShareButtonClick()
    {
        StartCoroutine(TakeSSAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();
        new NativeShare().SetSubject("Play Pipe Race!").SetText("Try this game: https://play.google.com/store/apps/details?id=com.DeVandD.PipeRace").Share();
    }
}