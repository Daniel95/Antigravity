using IoCPlus;
using UnityEngine;

public class CharacterDieView : View, ICharacterDie {

    [Inject] private Ref<ICharacterDie> characterDieRef;

    [Inject] private CharacterDieEvent characterDieEvent;

    [SerializeField] private string[] deadlyTags;

    private bool isInKillingTrigger;

    public override void Initialize() {
        characterDieRef.Set(this);
    }

    public void EnteringKillingTrigger(string killerTag)  {
        isInKillingTrigger = CheckDeadliness(killerTag);
    }

    public void ExitingKillingTrigger(string killerTag) {
        isInKillingTrigger = !CheckDeadliness(killerTag);
    }

    //when we collide with something while in the killing trigger.
    private void OnCollisionStay2D(Collision2D collision) {
        if (!isInKillingTrigger) { return; }

        isInKillingTrigger = false;
        characterDieEvent.Dispatch();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        for(int i = 0; i < deadlyTags.Length; i++) {
            if (collision.transform.CompareTag(deadlyTags[i])) {
                isInKillingTrigger = false;
                characterDieEvent.Dispatch();
            }
        }
    }

    /// <summary>
    /// check if this tag is deadly to us
    /// </summary>
    /// <param name="killerTag"></param>
    /// <returns></returns>
    private bool CheckDeadliness(string killerTag) {
        for (int i = 0; i < deadlyTags.Length; i++) {
            if (killerTag == deadlyTags[i]) {
                return true;
            }
        }

        return false;
    }
}
