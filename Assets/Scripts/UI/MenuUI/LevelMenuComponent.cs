using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelectMenu : MainMenuComponent
{
    public GameObject levelButtonPrefab; 
    public Transform scrollViewContent; 

    public List<LevelLayout> allLevels;
    private void Start() => PopulateLevelSelect();
    private void PopulateLevelSelect()
    {
        foreach (LevelLayout level in allLevels)
        {
            GameObject buttonObj = Instantiate(levelButtonPrefab, scrollViewContent);
            buttonObj.GetComponentInChildren<Text>().text = level.levelName;
            buttonObj.GetComponentInChildren<Image>().sprite = level.levelImage;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectLevel(level));
        }
    }

    private void SelectLevel(LevelLayout level)
    {
        // Burada seçilen seviye için yeni sahne başlatma işlemi yapılabilir
        Debug.Log("Selected Level: " + level.levelName);
        // SceneManager.LoadScene(level.levelConfig); // Örnek bir sahne yükleme işlemi
    }
}
