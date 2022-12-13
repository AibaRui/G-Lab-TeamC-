using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Image[] Image1;
    [SerializeField]
    private Image[] Image2;
    [SerializeField]
    private Image[] Image3;

    public static int[] image_Score1 = new int[6];

    private int[] imageScore1 = new int[6];


    private int[] Count = new int[6];
    // Start is called before the first frame update
    void Start()
    {
         imageScore1[0] = image_Score1[0];
         imageScore1[1] = image_Score1[1];
         imageScore1[2] = image_Score1[2];
        
    }

    // Update is called once per frame
    private void Update()
    {
      
            if (imageScore1[0] >= 1) { Image1[0].gameObject.SetActive(true); }
            if (imageScore1[0] >= 2) { Image1[1].gameObject.SetActive(true); }
            if (imageScore1[0] >= 3) { Image1[2].gameObject.SetActive(true); }
            if (imageScore1[0] >= 4) { Image1[3].gameObject.SetActive(true); }
            if (imageScore1[0] >= 5) { Image1[4].gameObject.SetActive(true); }
            if (imageScore1[0] >= 6) { Image1[5].gameObject.SetActive(true); }

            if (imageScore1[1] >= 1) { Image2[0].gameObject.SetActive(true); }
            if (imageScore1[1] >= 2) { Image2[1].gameObject.SetActive(true); }
            if (imageScore1[1] >= 3) { Image2[2].gameObject.SetActive(true); }
            if (imageScore1[1] >= 4) { Image2[3].gameObject.SetActive(true); }
            if (imageScore1[1] >= 5) { Image2[4].gameObject.SetActive(true); }
            if (imageScore1[1] >= 6) { Image2[5].gameObject.SetActive(true); }

            if (imageScore1[2] >= 1) { Image3[0].gameObject.SetActive(true); }
            if (imageScore1[2] >= 2) { Image3[1].gameObject.SetActive(true); }
            if (imageScore1[2] >= 3) { Image3[2].gameObject.SetActive(true); }
            if (imageScore1[2] >= 4) { Image3[3].gameObject.SetActive(true); }
            if (imageScore1[2] >= 5) { Image3[4].gameObject.SetActive(true); }
            if (imageScore1[2] >= 6) { Image3[5].gameObject.SetActive(true); }
        /*  if (imageScore1[1] >= 2) { Image1[1].gameObject.SetActive(true); }
          if (imageScore1[2] >= 3) { Image1[2].gameObject.SetActive(true); }
          if (imageScore1[3] >= 4) { Image1[3].gameObject.SetActive(true); }
          if (imageScore1[4] >= 5) { Image1[4].gameObject.SetActive(true); }
          if (imageScore1[5] >= 6) { Image1[5].gameObject.SetActive(true); }*/


    }
}
