using UnityEngine;
using UnityEngine.Analytics;
using UnknownSpace.Service;
using UnknownSpace.View;
using VContainer;

namespace UnknownSpace.Menu.View {
	public class MenuEntrypoint : MonoBehaviour {
		[SerializeField] LoginWindow _loginWindow;
		[SerializeField] RegisterWindow _registerWindow;
		[SerializeField] AlertWindow _alertWindow;

		BrainCloudService _brainCloudService;
		LevelService _levelService;

		[Inject]
		public void Init(BrainCloudService brainCloudService, LevelService levelService) {
			_brainCloudService = brainCloudService;
			_levelService = levelService;
		}

		void Awake() {
			_loginWindow.OnRegister += OnOpenRegister;
			_loginWindow.OnLogin += OnConfirmLogin;
			_registerWindow.OnBack += OnBackFromRegister;
			_registerWindow.OnRegister += OnConfirmRegister;
			_registerWindow.Hide();
			_alertWindow.Hide();
		}

		void OnDestroy() {
			_loginWindow.OnRegister -= OnOpenRegister;
			_loginWindow.OnLogin -= OnConfirmLogin;
			_registerWindow.OnBack -= OnBackFromRegister;
			_registerWindow.OnRegister -= OnConfirmRegister;
		}

		void Start() {
			Analytics.CustomEvent("load_menu");
		}


		void OnOpenRegister() {
			_loginWindow.Hide();
			_registerWindow.Show();
		}

		void OnConfirmLogin(string email, string password) {
			_loginWindow.MakeInactive();
			_brainCloudService.Login(
				email, password,
				() => _levelService.GoToMeta(),
				ShowError);
			_loginWindow.MakeActive();
		}

		void OnBackFromRegister() {
			_registerWindow.Hide();
			_loginWindow.Show();
		}

		void OnConfirmRegister(string email, string displayName, string password) {
			_registerWindow.MakeInactive();
			_brainCloudService.Register(
				email, displayName, password,
				() => _levelService.GoToMeta(),
				ShowError);
			_registerWindow.MakeActive();
		}

		void ShowError(string error) {
			_alertWindow.Init(error);
			_alertWindow.Show();
		}
	}
}