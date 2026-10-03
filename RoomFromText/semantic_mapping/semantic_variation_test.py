from semantic_mapper import (
    load_concepts,
    build_embeddings,
    find_candidates,
    format_candidates
)


TEST_CASES = [
    {
        "sentence": "A small red key is on the table.",
        "phrases": [
            ("small", "sizes"),
            ("red", "colors"),
            ("on", "relationships")
        ]
    },
    {
        "sentence": "A tiny crimson key is resting atop the table.",
        "phrases": [
            ("tiny", "sizes"),
            ("crimson", "colors"),
            ("atop", "relationships")
        ]
    }
]


if __name__ == "__main__":

    concepts = load_concepts()

    embeddings = build_embeddings(concepts)

    print("\nSEMANTIC VARIATION TEST\n")

    for test_case in TEST_CASES:

        print("=" * 60)

        print(
            "Sentence:",
            test_case["sentence"]
        )

        print("=" * 60)

        for phrase, category in test_case["phrases"]:

            candidates = find_candidates(
                phrase,
                category,
                embeddings,
                top_k=3
            )

            print(
                "\n" +
                format_candidates(
                    phrase,
                    category,
                    candidates
                )
            )