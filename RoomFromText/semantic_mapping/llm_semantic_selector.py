import os
import json
import requests


MODEL_ID = "meta-llama/Llama-3.1-8B-Instruct"

HF_TOKEN = os.getenv("HF_TOKEN")

if not HF_TOKEN:
    raise RuntimeError(
        "HF_TOKEN environment variable is not set."
    )


def select_canonical_concept(
    sentence,
    phrase,
    category,
    candidates
):

    candidate_names = [
        concept
        for concept, score in candidates
    ]

    prompt = f"""
You are a semantic normalization component
for a natural-language-to-game-scene system.

Your task is to select the canonical concept
that best represents a phrase in its sentence context.

Sentence:
{sentence}

Phrase:
{phrase}

Category:
{category}

Candidate canonical concepts:
{candidate_names}

Select exactly ONE concept from the candidate list.

Return ONLY valid JSON in this format:

{{
  "phrase": "{phrase}",
  "canonical_concept": "selected_concept"
}}

Do not return explanations.
Do not return Markdown.
Do not invent a concept that is not in the candidate list.
"""

    url = "https://router.huggingface.co/v1/chat/completions"

    headers = {
        "Authorization": f"Bearer {HF_TOKEN}",
        "Content-Type": "application/json"
    }

    payload = {
        "model": MODEL_ID,
        "messages": [
            {
                "role": "user",
                "content": prompt
            }
        ],
        "max_tokens": 100,
        "temperature": 0.0
    }

    response = requests.post(
        url,
        headers=headers,
        json=payload
    )

    if response.status_code != 200:
        raise RuntimeError(
            f"Hugging Face request failed: "
            f"{response.status_code}\n"
            f"{response.text}"
        )

    response_data = response.json()

    content = response_data["choices"][0]["message"]["content"]

    start = content.find("{")
    end = content.rfind("}")

    if start == -1 or end == -1:
        raise RuntimeError(
            "Could not extract JSON from LLM response."
        )

    json_text = content[start:end + 1]

    result = json.loads(json_text)

    if result["canonical_concept"] not in candidate_names:
        raise RuntimeError(
            "LLM returned a concept that was not in "
            "the candidate list: "
            + result["canonical_concept"]
        )

    return result