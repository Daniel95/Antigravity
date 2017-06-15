public interface IGameStateService {

    void Save(GameStateModel gameSave);
    GameStateModel Load();

}