#### room-from-text



**LLM-driven natural language to Unity scene generation prototype.**



Investigating how reliably LLMs can perform this language-to-executable-environment transformation, particularly with respect to structured representation, spatial consistency, and semantic correctness.

##### 

##### Current Pipeline



→ Natural-language scene description

→ LLM scene parser

→ Structured JSON scene representation

→ Unity scene generation

→Basic interaction

##### 

##### Scene Parser

The scene parser converts natural-language descriptions of simple game scenes into a structured JSON representation containing:



* Objects
* Object attributes
* Object states
* Spatial relationships
* Interaction rules



The parser prompt explicitly defines relationship attachment so that spatial relationships are assigned according to sentence structure and meaning rather than simply attaching them to the nearest noun.

###### 

##### Parser Evaluation

The parser was evaluated using 19 controlled natural-language test cases covering:



* Object extraction
* Object attributes and states
* Spatial relationships
* Relationship attachment
* Multiple objects of the same type
* Containment
* Reference resolution
* Interaction rules
* Multiple effects
* Complex scene descriptions
* Edge cases



The initial evaluation identified a relationship-attachment error. The parser prompt was subsequently refined to explicitly define relationship attachment and align relationship fields with the JSON representation.



The revised parser successfully handled the core scene-understanding requirements across the evaluation set.



The evaluation also identified limitations involving unsupported descriptive attributes, extension of the spatial-relation vocabulary, and negation/object absence. These are documented in the parser evaluation notes.



##### Project Status



###### Completed



* \[x] Natural-language scene parser
* \[x] Structured JSON scene representation
* \[x] Relationship attachment handling
* \[x] Parser evaluation and edge-case testing
* \[x] Unity scene generation
* \[x] Spatial relationships
* \[x] Color handling
* \[x] Basic material handling
* \[x] Basic interaction
* \[x] End-to-end prototype testing



###### Next



* \[ ] Improved spatial layout
* \[ ] More detailed object representations
* \[ ] More complex interaction rules





##### Current Limitations



The current prototype uses simple Unity primitives and approximate spatial placement. The scene generator currently supports a limited vocabulary of objects, attributes, and spatial relationships.



Future work includes improved spatial constraint handling, richer object representations, and more complex interaction rules.



##### Prototype Results



End-to-end test results are documented in - prototype\_results.md.

