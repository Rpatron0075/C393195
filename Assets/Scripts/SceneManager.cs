using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneChanger : MonoBehaviour
{
    public void GameOff()
    {
        SceneManager.LoadScene("Title");
    }
}
