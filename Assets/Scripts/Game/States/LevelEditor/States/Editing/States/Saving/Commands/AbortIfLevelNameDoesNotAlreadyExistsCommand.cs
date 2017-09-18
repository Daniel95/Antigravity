using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AbortIfLevelNameDoesNotAlreadyExistsCommand : Command {

    [InjectParameter] private string levelName;

    protected override void Execute() {
        DirectoryInfo info = new DirectoryInfo(LevelEditorLevelDataPath.Path);
        List<FileInfo> filesInfo = info.GetFiles().ToList();

        string levelNameInDirectory = StringHelper.ConvertToDirectoyFriendly(levelName);
        string levelFileName = levelNameInDirectory + ".xml";
        FileInfo fileInfoWithLevelName = filesInfo.Find(x => x.Name == levelFileName);

        if (fileInfoWithLevelName == null) {
            Abort();
        }
    }

}
