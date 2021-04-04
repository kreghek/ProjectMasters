using System;
using System.Linq;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class PersonCardHandler : MonoBehaviour
{
    public Person Person { get; set; }

    public PersonAvatarHandler PersonAvatarHandler;

    public Text NameText;
    public Text TraitText;
    public Text EffectsText;
    public Text SkillsText;

    public event EventHandler<EventArgs> PersonModalClicked;

    private void Start()
    {
        PersonAvatarHandler.Person = Person;
    }

    void Update()
    {
        NameText.text = Person.Name;
        TraitText.text = string.Join(", ", Person.Traits.Select(x => x.ToString()));
        EffectsText.text = string.Join(", ", Person.Effects.Select(x => x.EffectType.ToString()));
        SkillsText.text = string.Join("\n", Person.Skills.Select(x =>$"{x.Scheme.Sid}: {x.Level:0.#}"));
    }

    public void ModalClickButtonHandler()
    {
        PersonModalClicked?.Invoke(this, EventArgs.Empty);
    }
}
