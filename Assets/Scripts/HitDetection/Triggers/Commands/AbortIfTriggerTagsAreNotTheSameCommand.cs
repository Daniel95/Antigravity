using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class AbortIfTriggerTagsAreNotTheSameCommand : Command<List<string>> {

    [InjectParameter] private Collider2D collider;

    protected override void Execute(List<string> tags) {
        if(!tags.Contains(collider.tag)) {
            Abort();
        }
    }
}
