using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    public Transform questsTrans;
    int index;

    public Text textDescription;
    private Text cameraLabel;
    
    // Start is called before the first frame update
    void Start()
    {
       // cameraLabel = GameObject.Find("Canvas/Q").GetComponent<UnityEngine.UI.Text>();

        if (PlayerPrefs.HasKey("Mission"))
        {
            index = PlayerPrefs.GetInt("Mission");
        }
        else
        {
            index = 0;
            PlayerPrefs.SetInt("Mission", index);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        index = PlayerPrefs.GetInt("Mission");
      //  textDescription.text = quests[index].textDescr + quests[index].metersNeded.ToString();
        for (int i = 0; i < questsTrans.childCount; i++)
            if(i != index)
            {
                questsTrans.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                questsTrans.GetChild(index).gameObject.SetActive(true);
                if(textDescription != null)
                    textDescription.text = quests[index].textDescr + quests[index].metersNeded.ToString();
                else
                    cameraLabel.text = quests[index].textDescr + quests[index].metersNeded.ToString();
                if (quests[index].Complete == true)
                {

                    PlayerPrefs.SetInt("Mission", index + 1);
                }
            }
                
            

        
        // for(int i = 0)
    }

    public void DelLL() => PlayerPrefs.DeleteAll();
}
