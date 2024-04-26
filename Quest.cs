using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Quest : MonoBehaviour
{
  //  public int index;
    public int metersNeded;
    public int currentMeters;

  //  public Text textDescription;
    public string textDescr;

    public bool Complete = false;
    // Start is called before the first frame update
    void Start()
    {
       /* int currentIndex = PlayerPrefs.GetInt("Mission");
        if (index > currentIndex && index < PlayerPrefs.GetInt("Mission"))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Mission", index);
        }
       PlayerPrefs.SetInt("Mission", index); */
    }

    

    // Update is called once per frame
    void Update()
    {
//textDescription.text = textDescr;
        if(currentMeters >= metersNeded)
        {
            Complete = true;
        }
    }
}
