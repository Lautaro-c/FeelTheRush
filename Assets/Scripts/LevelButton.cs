using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int thisLevel;
    [SerializeField] private int lastLevelPlayed;
    [SerializeField] private GameObject blockLevelImage;
    [SerializeField] private Button levelButton;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TextMeshProUGUI alinesText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        levelButton = this.GetComponent<Button>();
        if (PlayerPrefs.HasKey("lastLvlPlayed"))
        {
            lastLevelPlayed = PlayerPrefs.GetInt("lastLvlPlayed");
        }
        else
        {
            lastLevelPlayed = 0;
        }
        if(blockLevelImage != null)
        {
            if (lastLevelPlayed >= thisLevel - 1)
            {
                blockLevelImage.SetActive(false);
            }else
            {
                blockLevelImage.SetActive(true);
                levelButton.onClick.RemoveAllListeners();
                levelButton.onClick.AddListener(() => 
                {
                    audioManager.PlayAudioClip(audioManager.clickCancel);
                });
            }
        }
        if (lastLevelPlayed >= thisLevel)
        {
            if (PlayerPrefs.HasKey($"lvl{thisLevel}Kills") && PlayerPrefs.HasKey($"lvl{thisLevel}Aliens"))
            {
                alinesText.text = PlayerPrefs.GetInt($"lvl{thisLevel}Kills").ToString() + "/" + PlayerPrefs.GetInt($"lvl{thisLevel}Aliens").ToString();
            }
            else
            {
                alinesText.text = "Error";
            }
            if(PlayerPrefs.HasKey($"lvl{thisLevel}Time"))
            {
                timeText.text = PlayerPrefs.GetFloat($"lvl{thisLevel}Time").ToString("F2");
            }
            else
            {
                timeText.text = "Error"; 
            }
            if (PlayerPrefs.HasKey($"lvl{thisLevel}Score"))
            {
                scoreText.text = PlayerPrefs.GetInt($"lvl{thisLevel}Score").ToString();
            }
            else
            {
                scoreText.text = "Error";
            }
        }
    }

}
