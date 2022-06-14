using UnityEngine.SceneManagement;

namespace UnknownSpace.Meta.Service {
	public sealed class LevelService {
		public void StartLevel() {
			SceneManager.LoadScene(1);
		}
	}
}