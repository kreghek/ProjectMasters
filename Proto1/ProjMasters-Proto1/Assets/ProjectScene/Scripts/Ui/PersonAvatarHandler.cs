using System.Collections;
using System.Collections.Generic;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class PersonAvatarHandler : MonoBehaviour
{
    public Image EyeImage;
    public Image FaceDecorImage;

    public Person Person { get; set; }

    void Update()
    {
        EyeImage.sprite = Resources.Load<Sprite>($"Persons/eye{Person.EyeIndex + 1}");
        if (Person.FaceDecorIndex > 0)
        {
            FaceDecorImage.sprite = Resources.Load<Sprite>($"Persons/face-decor{Person.FaceDecorIndex}");
        }
        else
        {
            FaceDecorImage.gameObject.SetActive(false);
        }
    }
}
