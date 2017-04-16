using System.Collections.Generic;

public interface ICharacterTriggerHitDetection {
    IEnumerable<string> CurrentTriggerTags { get; }
}
