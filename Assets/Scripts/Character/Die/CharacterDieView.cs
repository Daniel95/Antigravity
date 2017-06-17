using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDieView : View, ICharacterDie {

    public List<string> DeadlyTags { get { return deadlyTags; } }

    [Inject] private Ref<ICharacterDie> characterDieRef;

    [SerializeField] private List<string> deadlyTags;

    public override void Initialize() {
        characterDieRef.Set(this);
    }
}
