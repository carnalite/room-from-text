using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class LLMSceneParser : MonoBehaviour
{
    public string inputText;
    public string apiKey;
    public SceneGenerator sceneGenerator;

    [TextArea(10, 30)]
    public string sceneParserPrompt;

    private void Start()
    {
        ParseScene();
    }

    public void ParseScene()
    {
        Debug.Log("Input scene description: " + inputText);
        StartCoroutine(SendRequest());
    }

    IEnumerator SendRequest()
    {
        Debug.Log("Sending request to Hugging Face...");

        string url = "https://router.huggingface.co/v1/chat/completions";

        ChatRequest chatRequest = new ChatRequest();

        chatRequest.model = "meta-llama/Llama-3.1-8B-Instruct";
        chatRequest.max_tokens = 500;
        chatRequest.temperature = 0.1f;

        chatRequest.messages = new Message[2];

        chatRequest.messages[0] = new Message();
        chatRequest.messages[0].role = "system";
        chatRequest.messages[0].content = sceneParserPrompt;

        chatRequest.messages[1] = new Message();
        chatRequest.messages[1].role = "user";
        chatRequest.messages[1].content = inputText;

        string jsonBody = JsonUtility.ToJson(chatRequest);

        Debug.Log("Request JSON created successfully.");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(url, "POST");

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        Debug.Log("Request finished.");
        Debug.Log("Result: " + request.result);
        Debug.Log("Response Code: " + request.responseCode);
        Debug.Log("Response: " + request.downloadHandler.text);

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Hugging Face request SUCCESS!");

            ChatResponse chatResponse =
                JsonUtility.FromJson<ChatResponse>(request.downloadHandler.text);

            if (chatResponse == null ||
                chatResponse.choices == null ||
                chatResponse.choices.Length == 0)
            {
                Debug.LogError("Could not find LLM response content.");
                yield break;
            }

            string llmContent = chatResponse.choices[0].message.content;

            Debug.Log("LLM content: " + llmContent);

            string sceneJson = ExtractJson(llmContent);

            if (string.IsNullOrEmpty(sceneJson))
            {
                Debug.LogError("Could not extract JSON from LLM response.");
                yield break;
            }

            Debug.Log("Clean scene JSON: " + sceneJson);

            SceneData sceneData =
                JsonUtility.FromJson<SceneData>(sceneJson);

            if (sceneData == null ||
                sceneData.objects == null ||
                sceneData.relationships == null||
                sceneData.interactions == null)
            {
                Debug.LogError("LLM JSON does not match the expected SceneData structure.");
                yield break;
            }

            Debug.Log("LLM JSON parsed successfully.");

            if (sceneGenerator == null)
            {
                Debug.LogError("SceneGenerator reference is missing.");
                yield break;
            }

            sceneGenerator.GenerateScene(sceneJson);

            Debug.Log("Scene generated from LLM output.");
        }
        else
        {
            Debug.LogError("Hugging Face request FAILED!");
            Debug.LogError(request.error);
        }
    }

    private string ExtractJson(string text)
    {
        int start = text.IndexOf('{');
        int end = text.LastIndexOf('}');

        if (start == -1 || end == -1 || end <= start)
        {
            return null;
        }

        return text.Substring(start, end - start + 1);
    }
}

[System.Serializable]
public class ChatRequest
{
    public string model;
    public Message[] messages;
    public int max_tokens;
    public float temperature;
}

[System.Serializable]
public class Message
{
    public string role;
    public string content;
}

[System.Serializable]
public class ChatResponse
{
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public Message message;
}