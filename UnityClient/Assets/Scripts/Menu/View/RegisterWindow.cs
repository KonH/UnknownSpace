using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnknownSpace.View;

namespace UnknownSpace.Menu.View {
	public sealed class RegisterWindow : MonoBehaviour, IWindow, IInterativeWindow {
		[SerializeField] GameObject _root;
		[SerializeField] TMP_InputField _emailInput;
		[SerializeField] TMP_InputField _nameInput;
		[SerializeField] TMP_InputField _passwordInput;
		[SerializeField] Button _registerButton;
		[SerializeField] Button _backButton;

		public event Action<string, string, string> OnRegister;
		public event Action OnBack;

		public GameObject Root => _root;

		public Selectable[] Elements => new Selectable[] {
			_emailInput,
			_nameInput,
			_passwordInput,
			_registerButton,
			_backButton,
		};

		void Awake() {
			_registerButton.onClick.AddListener(() =>
				OnRegister?.Invoke(_emailInput.text, _nameInput.text, _passwordInput.text));
			_backButton.onClick.AddListener(() => OnBack?.Invoke());
		}
	}
}