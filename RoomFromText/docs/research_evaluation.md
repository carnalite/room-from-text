#### Research Evaluation



##### Objective



The evaluation investigates whether natural-language descriptions can be reliably transformed by an LLM into structured scene representations that Unity can interpret and execute.



##### Evaluation Levels



The prototype is evaluated at three levels:



###### Level 1 — Semantic Parsing



Does the LLM correctly identify:



\- objects

\- attributes

\- states

\- spatial relationships

\- references



###### Level 2 — Structured Representation



Does the generated JSON preserve the intended meaning?



This includes checking:



\- object identity

\- relationship subject and target

\- supported relationship types

\- completeness of referenced objects

\- consistency of attributes and states



###### Level 3 — Unity Execution



Does Unity generate an environment consistent with the structured representation?



This includes:



\- object creation

\- object attributes

\- spatial placement

\- object states

\- basic interaction



##### Test Set



The initial end-to-end evaluation contains 10 controlled scene descriptions covering basic scenes, multiple objects, spatial relationships, containment, states, references, negation, complex descriptions, and interaction.



Detailed observations will be added after testing.

