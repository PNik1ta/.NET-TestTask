public interface IPersonService {
	Task<long> Save(string json);
	Task<string> GetAll(GetAllRequest getAllRequest);
}