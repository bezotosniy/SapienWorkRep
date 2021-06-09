using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples
{
	public class MyTestRec3 : MonoBehaviour
	{
		private GCSpeechRecognition _speechRecognition;

		private Button _startRecordButton,
					   _stopRecordButton;

		private Text _resultText;			

		private Dropdown _microphoneDevicesDropdown;

	

		private Image _voiceLevelImage;
		public float max, current;

		private void Start()
		{
			_speechRecognition = GCSpeechRecognition.Instance;
			_speechRecognition.RecognizeSuccessEvent += RecognizeSuccessEventHandler;
			_speechRecognition.RecognizeFailedEvent += RecognizeFailedEventHandler;
			_speechRecognition.LongRunningRecognizeSuccessEvent += LongRunningRecognizeSuccessEventHandler;
			_speechRecognition.LongRunningRecognizeFailedEvent += LongRunningRecognizeFailedEventHandler;
			_speechRecognition.GetOperationSuccessEvent += GetOperationSuccessEventHandler;
			_speechRecognition.GetOperationFailedEvent += GetOperationFailedEventHandler;
			_speechRecognition.ListOperationsSuccessEvent += ListOperationsSuccessEventHandler;
			_speechRecognition.ListOperationsFailedEvent += ListOperationsFailedEventHandler;

			_speechRecognition.FinishedRecordEvent += FinishedRecordEventHandler;
			_speechRecognition.StartedRecordEvent += StartedRecordEventHandler;
			_speechRecognition.RecordFailedEvent += RecordFailedEventHandler;

			_speechRecognition.BeginTalkigEvent += BeginTalkigEventHandler;
			_speechRecognition.EndTalkigEvent += EndTalkigEventHandler;

			_startRecordButton = transform.Find("Canvas/Button_StartRecord").GetComponent<Button>();
			_stopRecordButton = transform.Find("Canvas/Button_StopRecord").GetComponent<Button>();
			
			_resultText = transform.Find("Canvas/Panel_ContentResult/Text_Result").GetComponent<Text>();

		
		
			_microphoneDevicesDropdown = transform.Find("Canvas/Dropdown_MicrophoneDevices").GetComponent<Dropdown>();		

		
			_voiceLevelImage = transform.Find("Canvas/Panel_VoiceLevel/Image_Level").GetComponent<Image>();

			_startRecordButton.onClick.AddListener(StartRecordButtonOnClickHandler);
			_stopRecordButton.onClick.AddListener(StopRecordButtonOnClickHandler);
		
			_microphoneDevicesDropdown.onValueChanged.AddListener(MicrophoneDevicesDropdownOnValueChangedEventHandler);

			_startRecordButton.interactable = true;
			_stopRecordButton.interactable = false;
		

			
			
			RefreshMicsButtonOnClickHandler();
		}
		private void Update()
		{
			if(_speechRecognition.IsRecording)
			{
				if (_speechRecognition.GetMaxFrame() > 0)
				{
					 max = (float)_speechRecognition.configs[_speechRecognition.currentConfigIndex].voiceDetectionThreshold;
					current = _speechRecognition.GetLastFrame() / max;

					if(current >= 1f)
					{
						_voiceLevelImage.fillAmount = Mathf.Lerp(_voiceLevelImage.fillAmount, Mathf.Clamp(current / 2f, 0, 1f), 30 * Time.deltaTime);
					}
					else
					{
						_voiceLevelImage.fillAmount = Mathf.Lerp(_voiceLevelImage.fillAmount, Mathf.Clamp(current / 2f, 0, 0.5f), 30 * Time.deltaTime);
					}

					_voiceLevelImage.color = current >= 1f ? Color.green : Color.red;
				}
			}
			else
			{
				_voiceLevelImage.fillAmount = 0f;
			}
		}

		private void RefreshMicsButtonOnClickHandler()
		{
			_speechRecognition.RequestMicrophonePermission(null);

			_microphoneDevicesDropdown.ClearOptions();

			for (int i = 0; i < _speechRecognition.GetMicrophoneDevices().Length; i++)
			{
				_microphoneDevicesDropdown.options.Add(new Dropdown.OptionData(_speechRecognition.GetMicrophoneDevices()[i]));

			}

			//smart fix of dropdowns

			_microphoneDevicesDropdown.value = PlayerPrefs.GetInt("SavedMic");
		}

		private void MicrophoneDevicesDropdownOnValueChangedEventHandler(int value)
		{
			if (!_speechRecognition.HasConnectedMicrophoneDevices())
				return;
			_speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[value]);
			PlayerPrefs.SetInt("SavedMic", value);
			//Debug.Log(value);
		}

		private void StartRecordButtonOnClickHandler()
		{
			_startRecordButton.interactable = false;
			_stopRecordButton.interactable = true;
		
			_resultText.text = string.Empty;
			StartCoroutine(StopRecordAuthomatic());
			_speechRecognition.StartRecord(false) ;
		}

		private void StopRecordButtonOnClickHandler()
		{
			_stopRecordButton.interactable = false;
			_startRecordButton.interactable = true;
		
			_speechRecognition.StopRecord();
		}
		private IEnumerator StopRecordAuthomatic()
		{
			while (true)
			{
				yield return new WaitForSeconds(1f);
				if (current < 0.07f)
				{
					yield return new WaitForSeconds(1f);
					if (current < 0.07f)
					{
						_speechRecognition.StopRecord();
						Debug.Log("StopRecord");

						_startRecordButton.interactable = true;
						yield break;
					}
				}
			}
		}

		private void GetOperationButtonOnClickHandler()
		{
			
		}

		private void GetListOperationsButtonOnClickHandler()
		{
			// some parameters could be seted
			_speechRecognition.GetListOperations();
		}

		private void DetectThresholdButtonOnClickHandler()
		{
			_speechRecognition.DetectThreshold();
		}

		private void CancelAllRequetsButtonOnClickHandler()
		{
			_speechRecognition.CancelAllRequests();
		}

		private void RecognizeButtonOnClickHandler()
		{
			if (_speechRecognition.LastRecordedClip == null)
			{
				_resultText.text = "<color=red>No Record found</color>";
				return;
			}

			FinishedRecordEventHandler(_speechRecognition.LastRecordedClip, _speechRecognition.LastRecordedRaw);
		}

		private void StartedRecordEventHandler()
		{
			
		}

		private void RecordFailedEventHandler()
		{
			_resultText.text = "<color=red>Start record Failed. Please check microphone device and try again.</color>";

			_stopRecordButton.interactable = false;
			_startRecordButton.interactable = true;
		}

		private void BeginTalkigEventHandler()
		{
			_resultText.text = "<color=blue>Talk Began.</color>";
		}

		private void EndTalkigEventHandler(AudioClip clip, float[] raw)
		{
			_resultText.text += "\n<color=blue>Talk Ended.</color>";

			FinishedRecordEventHandler(clip, raw);
		}

		private void FinishedRecordEventHandler(AudioClip clip, float[] raw)
		{
		
			if (clip == null)
				return;
			string Phrases = "phrase, phrase2, phrase3";
			RecognitionConfig config = RecognitionConfig.GetDefault();
			config.languageCode = (Enumerators.LanguageCode.en_US.Parse());
			config.speechContexts = new SpeechContext[]
			{
				new SpeechContext()
				{
					phrases = Phrases.Replace(" ", string.Empty).Split(',')
				}
			};
			config.audioChannelCount = clip.channels;
			// configure other parameters of the config if need

			GeneralRecognitionRequest recognitionRequest = new GeneralRecognitionRequest()
			{
				audio = new RecognitionAudioContent()
				{
					content = raw.ToBase64()
				},
				//audio = new RecognitionAudioUri() // for Google Cloud Storage object
				//{
				//	uri = "gs://bucketName/object_name"
				//},
				config = config
			};

				_speechRecognition.Recognize(recognitionRequest);
		
		}

		private void GetOperationFailedEventHandler(string error)
		{
			_resultText.text = "Get Operation Failed: " + error;
		}

		private void ListOperationsFailedEventHandler(string error)
		{
			_resultText.text = "List Operations Failed: " + error;
		}

		private void RecognizeFailedEventHandler(string error)
        {
            _resultText.text = "Recognize Failed: " + error;
        }

		private void LongRunningRecognizeFailedEventHandler(string error)
		{
			_resultText.text = "Long Running Recognize Failed: " + error;
		}

		private void ListOperationsSuccessEventHandler(ListOperationsResponse operationsResponse)
		{
			_resultText.text = "List Operations Success.\n";

			if (operationsResponse.operations != null)
			{
				_resultText.text += "Operations:\n";

				foreach (var item in operationsResponse.operations)
				{
					_resultText.text += "name: " + item.name + "; done: " + item.done + "\n";
				}
			}
		}

		private void GetOperationSuccessEventHandler(Operation operation)
		{
			_resultText.text = "Get Operation Success.\n";
			_resultText.text += "name: " + operation.name + "; done: " + operation.done;

			if(operation.done && (operation.error == null || string.IsNullOrEmpty(operation.error.message)))
			{
				InsertRecognitionResponseInfo(operation.response);
			}		
		}

		private void RecognizeSuccessEventHandler(RecognitionResponse recognitionResponse)
        {
			_resultText.text = "Recognize Success.";
			InsertRecognitionResponseInfo(recognitionResponse);
        }

        private void LongRunningRecognizeSuccessEventHandler(Operation operation)
        {
			if (operation.error != null || !string.IsNullOrEmpty(operation.error.message))
				return;

			_resultText.text = "Long Running Recognize Success.\n Operation name: " + operation.name;

			if (operation != null && operation.response != null && operation.response.results.Length > 0)
            {
                _resultText.text = "Long Running Recognize Success.";
				_resultText.text += "\n" + operation.response.results[0].alternatives[0].transcript;

				string other = "\nDetected alternatives:\n";

                foreach (var result in operation.response.results)
                {
                    foreach (var alternative in result.alternatives)
                    {
						if (operation.response.results[0].alternatives[0] != alternative)
						{
							other += alternative.transcript + ", ";
						}
                    }
                }

                _resultText.text += other;
            }
            else
            {
                _resultText.text = "Long Running Recognize Success. Words not detected.";
            }
        }

		private void InsertRecognitionResponseInfo(RecognitionResponse recognitionResponse)
		{
			if (recognitionResponse == null || recognitionResponse.results.Length == 0)
			{
				_resultText.text = "\nWords not detected.";
				return;
			}

			_resultText.text += "\n" + recognitionResponse.results[0].alternatives[0].transcript;

			var words = recognitionResponse.results[0].alternatives[0].words;

			if (words != null)
			{
				string times = string.Empty;

				foreach (var item in recognitionResponse.results[0].alternatives[0].words)
				{
					times += "<color=green>" + item.word + "</color> -  start: " + item.startTime + "; end: " + item.endTime + "\n";
				}

				_resultText.text += "\n" + times;
			}

			string other = "\nDetected alternatives: ";

			foreach (var result in recognitionResponse.results)
			{
				foreach (var alternative in result.alternatives)
				{
					if (recognitionResponse.results[0].alternatives[0] != alternative)
					{
						other += alternative.transcript + ", ";
					}
				}
			}

			_resultText.text += other;
		}
    }
}