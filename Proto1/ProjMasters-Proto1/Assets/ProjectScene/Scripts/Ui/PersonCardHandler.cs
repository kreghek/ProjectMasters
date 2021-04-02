using System.Linq;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class PersonCardHandler : MonoBehaviour
{
    public Person Person { get; set; }

    public Text NameText;
    public Text TraitText;
    public Text EffectsText;
    public Text SkillsText;

    public Image EyeImage;
    public Image FaceDecorImage;

    void Update()
    {
        NameText.text = Person.Name;
        TraitText.text = string.Join(", ", Person.Traits.Select(x => x.ToString()));
        EffectsText.text = string.Join(", ", Person.Effects.Select(x => x.EffectType.ToString()));
        SkillsText.text = string.Join("\n", Person.Skills.Select(x =>$"{x.Scheme.Sid}: {x.Level:0.#}"));

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
