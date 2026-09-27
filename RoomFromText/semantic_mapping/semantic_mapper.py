import os
import json
import requests
import numpy as np


MODEL_ID = "sentence-transformers/all-MiniLM-L6-v2"

HF_TOKEN = os.getenv("HF_TOKEN")

if not HF_TOKEN:
    raise RuntimeError(
        "HF_TOKEN environment variable is not set."
    )


def get_embedding(text):
    url = (
    "https://router.huggingface.co/hf-inference/models/"
    + MODEL_ID
    + "/pipeline/feature-extraction"
)

    headers = {
        "Authorization": f"Bearer {HF_TOKEN}",
        "Content-Type": "application/json"
    }

    payload = {
        "inputs": text,
        "normalize": True
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

    return np.array(response.json())


def cosine_similarity(vector_a, vector_b):
    return np.dot(vector_a, vector_b)


if __name__ == "__main__":

    phrase_a = "tiny"
    phrase_b = "small"

    print("Generating embedding for:", phrase_a)
    embedding_a = get_embedding(phrase_a)

    print("Generating embedding for:", phrase_b)
    embedding_b = get_embedding(phrase_b)

    similarity = cosine_similarity(
        embedding_a,
        embedding_b
    )

    print()
    print("Phrase A:", phrase_a)
    print("Phrase B:", phrase_b)
    print("Similarity:", similarity)