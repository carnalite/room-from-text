### Structured JSON Representation



The structured JSON representation acts as the intermediate representation between the LLM and Unity.

It describes the objects, their properties, spatial relationships, and interaction rules extracted from natural-language input.



#### Structure

The scene representation contains three main sections:

* Objects
* Relationships
* Interactions



#### Objects

Each object contains:

* `id` — unique identifier for the object
* `type` — type of object
* `size` — size when specified
* `state` — initial state when specified



#### Example:



```json

{

"id": "candle_1",

"type": "candle",

"size": "small",

"state": "unlit"

}




