using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AbortIfLevelIsNewButLevelNameAlreadyExistsCommand : Command {

    [Inject] private SavedLevelNameStatus levelNameStatus;

    [InjectParameter] private string levelName;

    protected override void Execute() {
        if(levelName == levelNameStatus.Name) { return; }

        DirectoryInfo info = new DirectoryInfo(LevelEditorLevelDataPath.Path);
        List<FileInfo> filesInfo = info.GetFiles().ToList();

        string levelNameInDirectory = StringHelper.ConvertToDirectoyCompatible(levelName);
        string levelFileName = levelNameInDirectory + ".xml";
        FileInfo fileInfoWithLevelName = filesInfo.Find(x => x.Name == levelFileName);

        if (fileInfoWithLevelName != null) {
            Abort();
        }
    }

}
