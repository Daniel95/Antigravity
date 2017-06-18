using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDieView : View, ICharacterDie {

    public List<string> DeadlyTags { get { return deadlyTags; } }

    [SerializeField] private List<string> deadlyTags;
}
