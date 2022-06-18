using UnityEngine.SceneManagement;

namespace UnknownSpace.Service {
	public sealed class LevelService {
		public void GoToMeta() {
			SceneManager.LoadScene(0);
		}

		public void StartLevel() {
			SceneManager.LoadScene(1);
		}
	}
}