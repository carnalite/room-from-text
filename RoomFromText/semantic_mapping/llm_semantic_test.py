from semantic_mapper import (
    load_concepts,
    build_embeddings,
    find_candidates
)

from llm_semantic_selector import (
    select_canonical_concept
)


TEST_CASES = [
    {
        "sentence":
            "A tiny crimson key is resting atop the table.",

        "phrases": [
            ("tiny", "sizes"),
            ("crimson", "colors"),
            ("atop", "relationships")
        ]
    },

    {
        "sentence":
            "A small candle is on top of a large table.",

        "phrases": [
            ("on top of", "relationships")
        ]
    },

    {
        "sentence":
            "A huge table is close to the chest.",

        "phrases": [
            ("huge", "sizes"),
            ("close to", "relationships")
        ]
    },

    {
        "sentence":
            "The key is within the chest.",

        "phrases": [
            ("within", "relationships")
        ]
    },

    {
        "sentence":
            "The chair is at the back of the table.",

        "phrases": [
            ("at the back of", "relationships")
        ]
    },

    {
        "sentence":
            "A little candle is close to the chest.",

        "phrases": [
            ("little", "sizes"),
            ("close to", "relationships")
        ]
    },

    {
        "sentence":
            "A tiny crimson key is resting atop a huge wooden table close to a locked chest.",

        "phrases": [
            ("tiny", "sizes"),
            ("crimson", "colors"),
            ("atop", "relationships"),
            ("huge", "sizes"),
            ("close to", "relationships")
        ]
    }
]


if __name__ == "__main__":

    concepts = load_concepts()

    embeddings = build_embeddings(concepts)

    print("\nLLM SEMANTIC SELECTION TEST\n")

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

            print("\nPhrase:", phrase)
            print("Category:", category)

            print("Candidates:")

            for concept, score in candidates:

                print(
                    f"  {concept}: {score:.4f}"
                )

            result = select_canonical_concept(
                test_case["sentence"],
                phrase,
                category,
                candidates
            )

            print(
                "LLM selection:",
                result["canonical_concept"]
            )