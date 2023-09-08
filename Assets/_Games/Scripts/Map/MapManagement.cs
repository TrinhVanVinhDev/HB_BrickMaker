using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class MapManagement : MonoBehaviour
{
    protected string pathFile = "Assets/_Games/Maps/map1.txt";
    protected DirectoryInfo folderMap = new DirectoryInfo(@"Assets/_Games/Maps");

    private void Start()
    {
        Oninit();
    }

    public virtual void Oninit()
    {
        //
    }
}
