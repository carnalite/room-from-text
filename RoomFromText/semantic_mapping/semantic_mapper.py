import os
import json
import requests
import numpy as np


MODEL_ID = "sentence-transformers/all-MiniLM-L6-v2"

CACHE_FILE = "embedding_cache.json"

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


def load_concepts():

    path = "canonical_concepts.json"

    with open(path, "r", encoding="utf-8") as file:
        return json.load(file)


def save_embeddings(embeddings):

    serializable_embeddings = {}

    for category, values in embeddings.items():

        serializable_embeddings[category] = {}

        for concept, embedding in values.items():

            serializable_embeddings[category][concept] = embedding.tolist()

    with open(CACHE_FILE, "w", encoding="utf-8") as file:

        json.dump(
            serializable_embeddings,
            file,
            indent=2
        )


def load_embeddings():

    with open(CACHE_FILE, "r", encoding="utf-8") as file:

        cached_data = json.load(file)

    embeddings = {}

    for category, values in cached_data.items():

        embeddings[category] = {}

        for concept, embedding in values.items():

            embeddings[category][concept] = np.array(
                embedding
            )

    return embeddings


def build_embeddings(concepts):

    if os.path.exists(CACHE_FILE):
        print("\nEmbedding cache found.")
        embeddings = load_embeddings()
    else:
        print("\nNo embedding cache found.")
        print("Starting a new embedding cache...\n")
        embeddings = {}

    for category, values in concepts.items():

        if category not in embeddings:
            embeddings[category] = {}

        for value in values:

            if value in embeddings[category]:
                print(
                    "Using cached embedding:",
                    category,
                    "->",
                    value
                )
                continue

            print(
                "Generating embedding for:",
                category,
                "->",
                value
            )

            embeddings[category][value] = get_embedding(value)

            # Save immediately after every new embedding
            save_embeddings(embeddings)

            print(
                "Saved:",
                category,
                "->",
                value
            )

    print("\nAll canonical embeddings are cached.")

    return embeddings


def find_best_match(
    phrase,
    category,
    embeddings,
    threshold=0.60
):

    phrase_embedding = get_embedding(phrase)

    best_concept = None
    best_score = -1

    for concept, concept_embedding in embeddings[category].items():

        score = cosine_similarity(
            phrase_embedding,
            concept_embedding
        )

        if score > best_score:

            best_score = score
            best_concept = concept

    accepted = best_score >= threshold

    return best_concept, best_score, accepted


if __name__ == "__main__":

    concepts = load_concepts()

    print("\nLoaded canonical concepts:")
    print(concepts)

    print("\nBuilding canonical embeddings...\n")

    embeddings = build_embeddings(concepts)

    test_phrases = [
        ("tiny", "sizes"),
        ("little", "sizes"),
        ("huge", "sizes"),
        ("crimson", "colors"),
        ("scarlet", "colors"),
        ("atop", "relationships"),
        ("on top of", "relationships"),
        ("close to", "relationships")
    ]

    print("\nSemantic mappings:\n")

    for phrase, category in test_phrases:

        concept, score, accepted = find_best_match(
            phrase,
            category,
            embeddings
        )

        if accepted:

            print(
                f"{phrase} -> {concept} "
                f"(score: {score:.4f})"
            )

        else:

            print(
                f"{phrase} -> {concept} "
                f"(score: {score:.4f}) "
                f"[below threshold]"
            )