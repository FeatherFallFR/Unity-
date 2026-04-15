using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //通关逻辑
            //打开胜利界面
            WinPanel.Instance.Show();
            Time.timeScale = 0;
        }
    }
}
