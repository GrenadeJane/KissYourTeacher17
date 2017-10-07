using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PUBLIC
{
    public static ScreenOrientation currentOrientation;
    public static Dictionary<string, Sprite> spriteDic;
    public delegate void CallBack(Texture2D texture);
    
    // Use this for initialization
    public static void Start () {
        spriteDic = new Dictionary<string, Sprite>();
        LoadSprite();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
  
    public static  void ChangerOrientationToLandscape()
    {
        currentOrientation = ScreenOrientation.Landscape;
    }

    public static void ChangeOrientationToPortrait()
    {
        currentOrientation = ScreenOrientation.Portrait;
    }

    static void LoadSprite()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");

        for (int i = 0; i < sprites.Length; i++)
        {
            spriteDic.Add(sprites[i].name, sprites[i]);
        }
    }

    public static IEnumerator LoadRemoteImage(string imageUrl, Action<Texture2D, int> method = null, int index = 0)
    {
        // Start a download of the given URL
        WWW www = new WWW(imageUrl);
        Debug.Log("www Print "); 
        // Wait for download to complete
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Error while downloading texture: " + www.error);
        }
        else
        {
            if (method != null) method(www.texture, index);
        }
    }

    public static bool CreateInstance<T>(ref T instance, T itSelf, GameObject obj )
    {
        if (instance == null)
        {
            instance = itSelf;
            obj.name = obj.name;
            Debug.Log("New instance of: " + obj.name + " assigned.");
            return true;
        }
        else if (!instance.Equals(itSelf))
        {
            Debug.Log("An instance of: " + obj.name + " already exists. Destroy the duplicate.");
            GameObject.DestroyImmediate(obj);
        }
        return false;
    }

    public static GameObject FindChildRecursiveStartWith(Transform lParent, string lName)
    {
        GameObject 
            lChild = null;
   
        foreach (Transform lTransform in lParent)
        {
            if (lTransform.name.StartsWith(lName))
            {
                return lChild = lTransform.gameObject;
            }
            else if ( lTransform.childCount != 0)
            {
               lChild = FindChildRecursiveStartWith(lTransform, lName);
                if (lChild != null) return lChild;
            }
        }

        return lChild;
    }

    // -- UTILITIES
    public static Color[] StringListtoColorTable(List<string> l_colorstringlist)
    {
        List<Color>
            l_ColorListReturn = new List<Color>();

        foreach (string color in l_colorstringlist)
        {
            l_ColorListReturn.Add(StringtoColor(color));
        }
        return l_ColorListReturn.ToArray();
    }

    public static Color StringtoColor(string l_scolor)
    {
        Color
            l_ColorReturn;
        ColorUtility.TryParseHtmlString(l_scolor, out l_ColorReturn);
        return l_ColorReturn;
    }
}
