using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLevels : MapManagement
{
    [SerializeField] private GameObject prefabsMaps;
    private Vector2 firstPoint = new Vector3(-220f, 480f);
    private Vector2 pointRenderj;
    private Vector2 pointRenderi;
    private GameObject mapRended;

    private float row;
    private int itemInRow;

    public static string textName
    {
        get { return textName; }
        set { textName = value; }
    }
    public override void Oninit()
    {
        base.Oninit();
        CreateListMaps();
    }

    private void CreateListMaps()
    {
        FileInfo[] arrFiles = folderMap.GetFiles("*.txt");
        pointRenderj = pointRenderi = firstPoint;

        row = Mathf.Round(arrFiles.Length / 3);
        int remainder;
        int loop = 3;
        int indexArrFile = 0;
        float quotient = Math.DivRem(arrFiles.Length, 3, out remainder);
        remainder = (remainder == 0) ? 3 : remainder;

        for (int i = 0; i <= row; i++)
        {
            if(i == row)
            {
                loop = remainder;
            }
            for (int j = 0; j < loop; j++)
            {
                mapRended = Instantiate(prefabsMaps, new Vector3(0f, 0f, 0f), transform.rotation);
                mapRended.transform.SetParent(transform);
                mapRended.GetComponent<RectTransform>().anchoredPosition = pointRenderj;
                textName = arrFiles[indexArrFile].Name;
                pointRenderj = pointRenderj + new Rect(220f, 0, 0, 0).position;
                indexArrFile++;
            }
            pointRenderi = pointRenderi - new Rect(0, 240f, 0, 0).position;
            pointRenderj = pointRenderi;
        }
    }
}
