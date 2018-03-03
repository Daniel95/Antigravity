using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AbortIfLevelIsNewButNameAlreadyExistsCommand : Command {

    [Inject] private LevelNameStatus levelNameStatus;

    [InjectParameter] private string levelName;

    protected override void Execute() {
        if(!File.Exists(LevelDataPath.Path)) { return; }
        if(levelName == levelNameStatus.Name) { return; }

        DirectoryInfo info = new DirectoryInfo(LevelDataPath.Path);
        List<FileInfo> filesInfo = info.GetFiles().ToList();

        string levelFileName = StringHelper.ConvertToXMLCompatible(levelName);
        FileInfo fileInfoWithLevelName = filesInfo.Find(x => x.Name == levelFileName);

        if (fileInfoWithLevelName != null) {
            Abort();
        }
    }

}
