##### Semantic Mapping Evaluation



###### Purpose



Evaluate whether embedding-based semantic similarity can identify linguistically different expressions that refer to the same canonical scene concept.



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



To be completed after the experiment.

