using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class States : MonoBehaviour
    {
		public static States instance { private set; get; }

		private List<GameState> m_states = new();
		public AudioSource enableSource => m_enableSource;
		public AudioSource disableSource => m_disableSource;

		[SerializeField] private AudioSource m_enableSource;
		[SerializeField] private AudioSource m_disableSource;
		[SerializeField] private GameState m_startState;

		private Stack<GameState> m_stack = new Stack<GameState>();
		private GameState m_currentState;

		private void Awake()
		{
			instance = this;

			GetComponentsInChildren(true, m_states);
		}

		private void Start()
		{
			m_states.ForEach(x=> x.gameObject.SetActive(false));

			m_currentState = m_startState;
			m_currentState.Enter();
		}

		public void Swap<T>() where T : GameState
		{
			//m_stack.Clear();

			var nextState = m_states.Find(x => x is T);
			if (nextState)
			{
				if (m_currentState)
				{
					m_currentState.Exit();
				}

				m_currentState = nextState;
				m_currentState.Enter();
				m_enableSource.Play();
				m_disableSource.Play();
			}
		}

		public void Push<T>() where T : GameState
		{
			Debug.Log($">>> Push state {typeof(T).Name} - m_currentState= {m_currentState}");

			var prevState =	m_currentState;

			Swap<T>();

			if (prevState)
			{
				m_stack.Push(prevState);
			}
		}

		public void Pop() 
		{
			Debug.Log($">>> pop state {m_stack.Count}");

			if (m_stack.Count == 0)
			{
				return;
			}

			if (m_currentState)
			{
				m_currentState.Exit();
				m_disableSource.Play();
			}

			m_currentState = m_stack.Pop();
			m_currentState.Enter();
			
		}
	}
}

