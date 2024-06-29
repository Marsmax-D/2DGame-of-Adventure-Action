using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject Puasemenu;
    public AudioMixer Audiomix;

    //开始键
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    //退出键
    public void Quit()
    {
        Application.Quit();
    }
    //开始菜单的显示
    public void UIstart()
    {
        GameObject.Find("Canvas/Menu/UI").SetActive(true);
    }
    //暂停菜单
    public void PuaseMenu()//暂停键
    {
        Puasemenu.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<PlayerControl>().Audiooff();//调用Playcontrol中的声音暂停函数
    }
    public void Return()//返回游戏键
    {
        Puasemenu.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<PlayerControl>().Audioon();//调用Playcontrol中的声音暂停函数
    }

    public void SetSound(float v)
    {
        Audiomix.SetFloat("MainVolume",v);//拖条控制BG音量
    }
    public void ReturnMenu()//返回主菜单键
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//通过返回主菜单键来返回上一个场景
        Time.timeScale = 1f;
    }
}
