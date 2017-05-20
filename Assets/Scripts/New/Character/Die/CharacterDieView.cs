using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDieView : View, ICharacterDie {

    public List<string> KillerTags { get { return killerTags; } }

    [Inject] private Ref<ICharacterDie> characterDieRef;

    [SerializeField] private List<string> killerTags;

    public override void Initialize() {
        characterDieRef.Set(this);
    }
}
