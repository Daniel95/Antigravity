using IoCPlus;

public class UpdateDeserializedLevelSaveDataCommand : Command {

    [Inject] private DeserializedLevelSaveDataStatus deserializedLevelSaveDataStatus;
    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        string levelFileName = StringHelper.ConvertToXMLCompatible(levelNameStatus.Name);
        LevelSaveData levelSaveData = SerializeHelper.Deserialize<LevelSaveData>(LevelDataPath.Path + levelFileName);
        deserializedLevelSaveDataStatus.LevelSaveData = levelSaveData;
    }

}
