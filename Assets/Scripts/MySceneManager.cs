using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    // contoh pembuatan variable di unity
    /* public string characterString;
    public int bilangBulat;
    public float bilangPecahan;
    public bool kondisi; */

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello world!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
