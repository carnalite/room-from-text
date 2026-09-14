#### System Architecture



The Room-from-Text prototype follows a pipeline that converts natural-language descriptions into interactive game scenes.



##### Overall Pipeline



Natural Language Input

→ LLM-based Scene Parser

→ Structured JSON Representation

→ Unity Scene Generator

→ Interactive Game Scene



###### 1\. Natural Language Input



The user provides a natural-language description of a simple game scene.



Example:



"A small candle is on a large table near a locked chest."



###### 2\. LLM-based Scene Parser



The language model interprets the natural-language input and extracts relevant semantic information.



The parser identifies:



\- Objects

\- Object properties such as size

\- Object states

\- Spatial relationships

\- Interaction rules



The LLM does not directly generate the Unity scene.



Instead, it converts the natural-language description into a structured representation.



###### 3\. Structured JSON Representation



The extracted information is represented using a predefined JSON structure.



The JSON acts as an intermediate representation between the language model and Unity.



This separates language understanding from scene generation.



###### 4\. Unity Scene Generator



Unity reads the structured JSON representation and converts the described objects and relationships into a game scene.



For example:



\- An object type is mapped to a corresponding Unity **primitive** or prefab.

\- Object properties are used to determine characteristics such as **scale**.

\- Spatial relationships are used to determine object **placement**.

\- Object states are used to initialize the corresponding **state** in the scene.



###### 5\. Interaction System



Interaction rules extracted from the natural-language description are used to create simple gameplay interactions.



For example:



"Lighting the candle unlocks the chest."



can result in:



Light candle

→ Change chest state from locked to unlocked.



##### Design Principle



The prototype separates the responsibilities of the language model and the game engine.



LLM:

**Understands** and structures natural language.



JSON:

**Represents** the extracted scene semantics.



Unity:

**Generates** and controls the actual game scene.



This separation allows the natural-language interpretation component and the scene-generation component to be developed and evaluated independently.

