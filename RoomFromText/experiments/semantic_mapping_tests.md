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



###### Candidate Retrieval



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

