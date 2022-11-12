using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Image background;

    private void Start()
    {
        background.sprite = GameManager.Instance.currentLevel.backgroundLevel;
    }
}
