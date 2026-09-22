#### room-from-text



**An LLM-driven prototype that converts natural-language scene descriptions into structured representations and**

**generates executable Unity environments.**



**The system uses an intermediate JSON representation between language understanding and Unity scene generation.**



##### Current Pipeline



→ Natural-language scene description

→ LLM scene parser

→ Structured JSON scene representation

→ Scene Validation

→ Unity scene generation

→ Basic interaction

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
* \[x] Object attributes
* \[x] Spatial relationships
* \[x] LLM-generated interaction rules
* \[x] Data-driven interaction execution
* \[x] Scene regeneration
* \[x] Basic scene validation
* \[x] End-to-end prototype



###### Next



* \[ ] Richer object representations
* \[ ] Improved spatial constraint handling
* \[ ] Broader executable interaction capabilities
* \[ ] Systematic evaluation of LLM reliability





##### Current Limitations



The current prototype uses Unity primitives and a limited vocabulary of object types, spatial relationships, and interaction actions.



The generated spatial placement is approximate and the interaction executor currently supports a limited set of predefined executable actions.



##### Prototype Results



End-to-end test results are documented in - prototype\_results.md.

