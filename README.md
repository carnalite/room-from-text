# \# room-from-text

# 

# LLM-driven natural language to Unity scene generation prototype.

# 

# \## Current Pipeline

# 

# Natural-language scene description

# → LLM scene parser

# → Structured JSON scene representation

# 

# \## Scene Parser

# 

# The scene parser converts natural-language descriptions of simple game scenes into a structured JSON representation containing:

# 

# \* Objects

# \* Object attributes

# \* Object states

# \* Spatial relationships

# \* Interaction rules

# 

# The parser prompt explicitly defines relationship attachment so that spatial relationships are assigned according to sentence structure and meaning rather than simply attaching them to the nearest noun.

# 

# \## Parser Evaluation

# 

# The parser was evaluated using 19 controlled natural-language test cases covering:

# 

# \* Object extraction

# \* Object attributes and states

# \* Spatial relationships

# \* Relationship attachment

# \* Multiple objects of the same type

# \* Containment

# \* Reference resolution

# \* Interaction rules

# \* Multiple effects

# \* Complex scene descriptions

# \* Edge cases

# 

# The initial evaluation identified a relationship-attachment error. The parser prompt was subsequently refined to explicitly define relationship attachment and align relationship fields with the JSON representation.

# 

# The revised parser successfully handled the core scene-understanding requirements across the evaluation set.

# 

# The evaluation also identified limitations involving unsupported descriptive attributes, extension of the spatial-relation vocabulary, and negation/object absence. These are documented in the parser evaluation notes.

# 

# \## Project Status

# 

# \### Completed

# 

# \* \[x] Natural-language scene parser

# \* \[x] Structured JSON scene representation

# \* \[x] Relationship attachment handling

# \* \[x] Parser evaluation and edge-case testing

# 

# \### Next

# 

# \* \[ ] Convert parsed JSON into Unity scene objects

# \* \[ ] Generate spatial relationships in Unity

# \* \[ ] Apply object states

# \* \[ ] Support interaction rules

# \* \[ ] Demonstrate end-to-end natural-language-to-game-scene generation



