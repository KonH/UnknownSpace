using TMPro;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.View;

namespace UnknownSpace.Menu.View {
	public sealed class LoginWindow : MonoBehaviour, IWindow, IInterativeWindow {
		[SerializeField] GameObject _root;
		[SerializeField] TMP_InputField _emailInput;
		[SerializeField] TMP_InputField _passwordInput;
		[SerializeField] Button _loginButton;
		[SerializeField] Button _registerButton;

		public event Action<string, string> OnLogin;
		public event Action OnRegister;

		public GameObject Root => _root;

		public Selectable[] Elements => new Selectable[] {
			_emailInput,
			_passwordInput,
			_loginButton,
			_registerButton,
		};

		void Awake() {
			_loginButton.onClick.AddListener(() =>
				OnLogin?.Invoke(_emailInput.text, _passwordInput.text));
			_registerButton.onClick.AddListener(() => OnRegister?.Invoke());
		}
	}
}