#### Research Problem



##### Motivation



Natural-language descriptions provide an intuitive way for humans to communicate information about environments. Large language models can process such descriptions and generate structured information, creating the possibility of using natural language as an interface for constructing interactive environments.



However, converting natural-language descriptions into executable game environments is not simply a text-generation problem.



A generated scene representation must preserve the meaning of the original description, including object identities, attributes, states, spatial relationships, and interactions. Errors in these elements can result in an environment that is syntactically valid but semantically incorrect.



For example, the description: 



"A candle is on a table near a chest."



requires the system to distinguish between:



candle → on → table



and



candle → near → chest



rather than assigning the second relationship to the wrong object.



Similarly, a statement such as:



"A small key is inside a locked chest."



requires the system to represent both the containment relationship and the state of the chest.



These examples illustrate the gap between generating structured data and generating a semantically consistent executable environment.



##### Research Problem



This idea investigates the reliability of transforming natural-language descriptions into structured representations that can be interpreted and executed as interactive game environments.



The prototype focuses on an intermediate representation between the language model and the game engine. Natural-language descriptions are converted by an LLM into structured JSON containing objects, attributes, states, and spatial relationships. Unity then interprets this representation to generate the corresponding environment.



###### Why Research Is Needed



A simple demonstration can show that an LLM is capable of producing a scene representation and that Unity can generate objects from it. However, this does not establish that the generated environments are consistently correct.



Important questions remain regarding:



\- **semantic correctness** of extracted objects and attributes

\- correct attachment of **spatial relationships**

\- **consistency** of spatial constraints

\- handling of ambiguous or **complex descriptions**

\- **reference resolution**

\- **negation** and object absence

\- invalid or **incomplete generated representations**

\- **reliability** as scene complexity increases



These issues require systematic evaluation rather than a single successful

demonstration.



###### Research Questions



**RQ1**



How accurately can an LLM extract objects, attributes, states, and spatial relationships from natural-language descriptions of game scenes?



**RQ2**



How reliably can natural-language spatial relationships be represented as structured constraints for an executable environment?



**RQ3**



What types of errors occur when LLM-generated scene representations are translated into a game engine?



**RQ4**



How does the reliability of the generated representation change as scene descriptions become more complex?



###### Initial Approach



The current prototype uses the following pipeline:



Natural-language description

&#x20;       ↓

LLM-based scene parser

&#x20;       ↓

Structured JSON representation

&#x20;       ↓

Unity scene generator

&#x20;       ↓

Generated interactive scene



The structured representation acts as an intermediate layer between the language model and the game engine, allowing the generated interpretation to be examined before or during execution.



###### Initial Evaluation



The scene parser was previously evaluated using 19 controlled test cases covering object extraction, attributes, states, spatial relationships, relationship attachment, containment, reference resolution, interaction rules, complex scenes, and edge cases.



The evaluation demonstrated successful handling of the core scene-parsing requirements while also identifying limitations involving unsupported descriptive attributes, spatial-relation vocabulary, and negation/object

absence.



The current end-to-end evaluation extends this investigation by testing the actual LLM-to-Unity pipeline.



##### Future Research



Future investigation may explore improved spatial constraint reasoning, validation of LLM-generated scene representations, handling of ambiguous language, richer environment representations, and more complex interactive behaviors.

