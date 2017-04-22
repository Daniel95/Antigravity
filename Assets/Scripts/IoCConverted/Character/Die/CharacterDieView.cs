using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDieView : View, ICharacterDie {

    public List<string> KillerTag { get { return killerTag; } }

    [Inject] private Ref<ICharacterDie> characterDieRef;

    [SerializeField] private List<string> killerTag;

    public override void Initialize() {
        characterDieRef.Set(this);
    }
}
