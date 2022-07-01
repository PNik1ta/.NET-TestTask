/// <summary>
/// Repository interface for Person Entity
/// </summary>
public interface IPersonRepository {
	Task<long> Save(string json);

	Task<string> GetAll(GetAllRequest getAllRequest);
}