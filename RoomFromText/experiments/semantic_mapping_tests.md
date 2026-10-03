##### Semantic Mapping Evaluation



###### Purpose



Evaluate whether Hugging Face embedding-based semantic retrieval can retrieve canonical concepts for linguistically varied expressions.



###### Model



sentence-transformers/all-MiniLM-L6-v2



###### Method



Text phrases are converted into embeddings using Hugging Face Inference Providers. Cosine similarity is then calculated between the resulting embeddings.



###### Positive pairs



tiny            <-> small           : 0.9124

little          <-> small           : 0.7237

big             <-> large           : 0.8065

huge            <-> large           : 0.8102

crimson         <-> red             : 0.5131

scarlet         <-> red             : 0.3757

atop            <-> on              : 0.3264

on top of       <-> on              : 0.5352

close to        <-> near            : 0.7334



###### Negative pairs



small           <-> large           : 0.7869

red             <-> blue            : 0.7294

key             <-> table           : 0.3522

on              <-> inside          : 0.3229

behind          <-> near            : 0.4559



###### Observation



* The results show that raw embedding similarity does not reliably distinguish semantic equivalence from broader semantic relatedness.
* For example, the similarity between `small` and `large` was 0.7869, despite these concepts representing different values within the same size category. Similarly, `red` and `blue` produced a similarity of 0.7294.
* Therefore, a universal similarity threshold should not be treated as a synonym classifier.
* The semantic mapping layer is instead being developed as a candidate-retrieval mechanism, with contextual interpretation handled later by the LLM.



##### Candidate Retrieval



Results from the top-k candidate retrieval experiment will be added below.



**tiny -> sizes**

&#x20;   small: 0.9124

&#x20;   large: 0.6852

&#x20;   medium: 0.5268

The correct concept was ranked first.



**little -> sizes**

&#x20;   small: 0.7237

&#x20;   large: 0.4006

&#x20;   medium: 0.3777

The correct concept was ranked first.



**huge -> sizes**

&#x20;   large: 0.8102

&#x20;   small: 0.6491

&#x20;   medium: 0.3689

The correct concept was ranked first.



**crimson -> colors**

&#x20;   red: 0.5131

&#x20;   blue: 0.4254

&#x20;   green: 0.4002

The correct concept was ranked first, although the similarity difference from other color concepts was relatively small.



**scarlet -> colors**

&#x20;   red: 0.3757

&#x20;   blue: 0.3645

&#x20;   yellow: 0.3628

The correct concept was ranked first, but the scores were very close.



**atop -> relationships**

&#x20;   on: 0.3264

&#x20;   beside: 0.3076

&#x20;   inside: 0.2949

The correct concept was ranked first, but with a low similarity score.



**on top of -> relationships**

&#x20;   beside: 0.5511

&#x20;   on: 0.5352

&#x20;   behind: 0.4949

The expected concept was not ranked first. This demonstrates that embedding similarity alone cannot reliably determine the intended canonical spatial relation.



**close to -> relationships**

&#x20;   near: 0.7334

&#x20;   beside: 0.4411

&#x20;   behind: 0.4257

The correct concept was ranked first with a clear score difference.



###### Observation



* Candidate retrieval performs well for several lexical variations, particularly size expressions such as tiny, little, and huge.
* However, performance is less reliable for color and spatial relationship expressions.
* The on top of example is particularly important because the semantically intended concept on was not the highest-ranked embedding candidate.
* This supports using embedding similarity as a candidate retrieval mechanism rather than as the final semantic classifier.
* The next stage will use the retrieved candidates together with the full scene context and an LLM to determine the intended canonical concept.



##### LLM-Assisted Candidate Selection



###### **Test-1**



The candidate retrieval stage was combined with contextual LLM selection.



The LLM received:

* the full sentence
* the phrase being normalized
* the semantic category
* the top-k embedding candidates



The LLM was constrained to select one concept from the retrieved candidate list.



###### Results



Results are recorded here after running the LLM semantic selection experiments.



============================================================

Sentence: A tiny crimson key is resting atop the table.

============================================================

Phrase: tiny

Category: sizes

Candidates:

&#x20; small: 0.9124

&#x20; large: 0.6852

&#x20; medium: 0.5268

LLM selection: small



Phrase: crimson

Category: colors

Candidates:

&#x20; red: 0.5131

&#x20; blue: 0.4254

&#x20; green: 0.4002

LLM selection: red



Phrase: atop

Category: relationships

Candidates:

&#x20; on: 0.3264

&#x20; beside: 0.3076

&#x20; inside: 0.2949

LLM selection: on



============================================================

Sentence: A small candle is on top of a large table.

============================================================

Phrase: on top of

Category: relationships

Candidates:

&#x20; beside: 0.5511

&#x20; on: 0.5352

&#x20; behind: 0.4949

LLM selection: on



============================================================

Sentence: A huge table is close to the chest.

============================================================

Phrase: huge

Category: sizes

Candidates:

&#x20; large: 0.8102

&#x20; small: 0.6491

&#x20; medium: 0.3689

LLM selection: large



Phrase: close to

Category: relationships

Candidates:

&#x20; near: 0.7334

&#x20; beside: 0.4411

&#x20; behind: 0.4257

LLM selection: near



###### **Test-2**



###### Results



============================================================

Sentence: A tiny crimson key is resting atop the table.

============================================================

Phrase: tiny

Category: sizes

Candidates:

&#x20; small: 0.9124

&#x20; large: 0.6852

&#x20; medium: 0.5268

LLM selection: small



Phrase: crimson

Category: colors

Candidates:

&#x20; red: 0.5131

&#x20; blue: 0.4254

&#x20; green: 0.4002

LLM selection: red



Phrase: atop

Category: relationships

Candidates:

&#x20; on: 0.3264

&#x20; beside: 0.3076

&#x20; inside: 0.2949

LLM selection: on



============================================================

Sentence: A small candle is on top of a large table.

============================================================

Phrase: on top of

Category: relationships

Candidates:

&#x20; beside: 0.5511

&#x20; on: 0.5352

&#x20; behind: 0.4949

LLM selection: on



============================================================

Sentence: A huge table is close to the chest.

============================================================

Phrase: huge

Category: sizes

Candidates:

&#x20; large: 0.8102

&#x20; small: 0.6491

&#x20; medium: 0.3689

LLM selection: large



Phrase: close to

Category: relationships

Candidates:

&#x20; near: 0.7334

&#x20; beside: 0.4411

&#x20; behind: 0.4257

LLM selection: near



============================================================

Sentence: The key is within the chest.

============================================================

Phrase: within

Category: relationships

Candidates:

&#x20; inside: 0.8243

&#x20; near: 0.5420

&#x20; beside: 0.5107

LLM selection: inside



============================================================

Sentence: The chair is at the back of the table.

============================================================

Phrase: at the back of

Category: relationships

Candidates:

&#x20; behind: 0.6258

&#x20; near: 0.5233

&#x20; beside: 0.5216

LLM selection: behind



============================================================

Sentence: A little candle is close to the chest.

============================================================

Phrase: little

Category: sizes

Candidates:

&#x20; small: 0.7237

&#x20; large: 0.4006

&#x20; medium: 0.3777

LLM selection: small



Phrase: close to

Category: relationships

Candidates:

&#x20; near: 0.7334

&#x20; beside: 0.4411

&#x20; behind: 0.4257



###### Observation



The experiments show that embedding similarity is useful for retrieving a small set of candidate canonical concepts, but it should not be treated as a final synonym classifier.



Several expressions were correctly ranked by embedding similarity, including tiny, little, huge, and close to.



However, the on top of example showed that the highest similarity candidate can be semantically incorrect for the intended scene relationship.



The contextual LLM was able to select on when given the complete sentence and the retrieved candidate concepts.



Therefore, the semantic mapping stage is designed as a two-step process:

1. Embedding-based candidate retrieval.
2. Contextual LLM selection of the canonical concept.



This provides a controlled semantic normalization layer before structured scene generation.



###### Limitation



* The LLM experiments were limited by the monthly included Hugging Face Inference Provider credits. The remaining test requests could not all be repeated after the account reached its included usage limit.
* The documented results therefore represent the successful experiments that were completed rather than a large-scale evaluation.
* Further evaluation can be performed later using additional inference capacity.

