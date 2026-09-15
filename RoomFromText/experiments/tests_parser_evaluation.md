#### Scene Parser Evaluation



##### 1. Purpose



This document records the evaluation of the natural-language scene parser used in the natural-language-to-game-scene generation prototype.

The evaluation tests whether the parser can convert natural-language descriptions into the structured JSON representation defined by `json_schema.md`.



The tests cover:



* Object extraction
* Object attributes
* Object states
* Spatial relationships
* Relationship attachment
* Multiple objects of the same type
* Containment relationships
* Reference resolution
* Interaction rules
* Multiple effects
* Complex combined scenes
* Edge cases and limitations



---



##### 2. Initial Test



###### Test 01 — Initial Prompt



Input



**A small candle is on a large table near a locked chest.**



Initial Result



The initial parser correctly identified the three objects and their properties.

However, it produced the following relationship interpretation:

* candle → on → table
* table → near → chest



The expected interpretation was:

* candle → on → table
* candle → near → chest



The initial output also used `type` and `object` as relationship fields instead of the schema-defined `relation` and `target` fields.



Initial Evaluation



* Objects: Correct
* Object properties: Correct
* Relationship attachment: Incorrect
* Schema compliance: Incorrect
* Hallucinated information: No
* Overall: Partially correct



Prompt Revision



1. The scene parser prompt was revised to explicitly define relationship attachment.
2. The revised prompt states that relationships must be attached according to sentence structure and meaning rather than automatically being assigned to the nearest noun.
3. The relationship fields were also explicitly aligned with the JSON representation:



* `subject`
* `relation`
* `target`



Retest Result



After the prompt revision, the same scene was parsed correctly.

The resulting relationships were:

* candle → on → table
* candle → near → chest



This demonstrates that the prompt revision addressed the relationship-attachment error identified in the initial test.



---



##### 3. Evaluation Tests



###### Test 02 — Attributes and Containment



Input



**A red book is inside a wooden chest, and the chest is beside a small table.**



Evaluation



* Objects: Correct
* Attributes: Correct
* Containment: Correct
* Relationship attachment: Correct
* Schema structure: Correct
* Hallucinated information: No
* Overall: Pass



---



###### Test 03 — Multiple Spatial Relationships



Input



**A key is on a small table near a locked chest behind a large chair.**



Evaluation



* Objects: Correct
* Size/state attributes: Correct
* Spatial relationships: Correct
* Relationship attachment: Correct
* Hallucinated information: No
* Overall: Pass



---



###### Test 04 — Multiple Objects of the Same Type



Input



**A red apple is on a small table, and a green apple is near a large table.**



Evaluation



* Multiple objects of the same type: Correct
* Object identification: Correct
* Attributes: Correct
* Relationship attachment: Correct
* Object IDs: Correctly differentiated
* Overall: Pass



---



###### Test 05 — Nested Relationships



Input



**A key is inside a wooden chest, which is behind a large table near a locked door.**



Evaluation



* Objects: Correct
* Attributes: Correct
* Containment: Correct
* Multiple relationships: Correct
* Relationship attachment: Correct
* Overall: Pass



---



###### Test 06 — Multiple Relationships and Attachment



Input



**A small book is on a large table beside a candle, while a chair is behind the table.**



Evaluation



* Objects: Correct
* Size attributes: Correct
* Multiple relationships: Correct
* Relationship attachment: Correct
* Overall: Pass



---



###### Test 07 — Attributes and Containment



Input



**A blue key is inside a small locked chest on a wooden table.**



Evaluation



* Objects: Correct
* Color: Correct
* Size: Correct
* Material: Correct
* State: Correct
* Containment: Correct
* Spatial relationship: Correct
* Overall: Pass



---



###### Test 08 — Basic Interaction



Input



**When the player opens the locked chest, the door becomes unlocked.**



Evaluation



* Objects: Correct
* Initial state: Correct
* Trigger: Correct
* Effect: Correct
* Overall: Pass



---



###### Test 09 — Interaction and Containment



Input



**A key is inside a locked chest. When the player takes the key, the chest becomes unlocked.**



Evaluation



* Objects: Correct
* State: Correct
* Containment: Correct
* Trigger: Correct
* Effect: Correct
* Overall: Pass



---



###### Test 10 — Complex Scene



Input



**A small red key is on a wooden table near a locked chest, while a large chair is behind the chest.**



Evaluation



* Objects: Correct
* Multiple attributes: Correct
* Multiple relationships: Correct
* Relationship attachment: Correct
* Overall: Pass



---



###### Test 11 — Minimal Scene



Input



**A candle is on a table.**



Evaluation



* Objects: Correct
* Relationship: Correct
* No unnecessary information: Correct
* Overall: Pass



---



###### Test 12 — Unsupported Descriptive Information



Input



**A beautiful glowing candle is magically floating above a table.**



Evaluation



* Objects: Correct
* Main spatial concept: Correctly interpreted
* Unsupported descriptive attributes: Not represented
* Relationship vocabulary: The model generated `above`, although it was not explicitly listed in the parser prompt
* Overall: Partial / Limitation identified



Limitation



* The current representation supports a controlled set of attributes and spatial relationships. Descriptive terms such as "beautiful", "glowing", and "magically" are not currently represented.
* The model also generalized the spatial relationship vocabulary by producing `above`.
* This indicates that the representation should eventually define whether additional spatial relations and descriptive attributes are supported.



---



###### Test 13 — Reference Resolution



Input



**A key is inside a locked chest. It is behind a large table.**



Evaluation



* Objects: Correct
* State: Correct
* Containment: Correct
* Pronoun resolution: Correct
* Relationship attachment: Correct
* Overall: Pass



The parser interpreted "It" as referring to the chest.



---



###### Test 14 — Multiple References



Input



**A red key is on a small table. The table is behind a locked chest, and it is near a large chair.**



Evaluation



* Objects: Correct
* Attributes: Correct
* Reference resolution: Correct
* Multiple relationships: Correct
* Relationship attachment: Correct
* Overall: Pass



The parser correctly associated both relationships with the table.



---



###### Test 15 — Object and Floor Relationship



Input



**A small blue key is lying on the floor.**



Evaluation



* Objects: Correct
* Attributes: Correct
* Floor representation: Correct
* Spatial relationship: Correct
* Overall: Pass



---



###### Test 16 — Multiple Relationships from a Scene



Input



**A small red key is on a wooden table near a locked chest behind a large chair.**



Evaluation



* Objects: Correct
* Attributes: Correct
* Multiple relationships: Correct
* Relationship attachment: Correct
* Overall: Pass



---



###### Test 17 — Multiple Effects from One Trigger



Input



**When the player opens the chest, the chest becomes unlocked and the key inside becomes accessible.**



Evaluation



* Objects: Correct
* Trigger: Correct
* Multiple effects: Correct
* State change: Correct
* Overall: Pass



---



###### Test 18 — Negation and Absence



Input



**A candle is not on the table, and there is no key inside the chest.**



Evaluation



* Objects: Partially correct
* Negated relationship: Recognized
* Absence of object: Not represented cleanly
* Object reference: Invalid for the key because no `key_1` object was created
* Overall: Partial / Limitation identified



Limitation



* The current representation does not yet define a dedicated representation for object absence or negated facts.
* The parser represented negated relationships using `not_on` and `not_inside`, but the second relationship referenced `"key"` rather than a valid object ID.
* A future schema revision may define explicit support for negation and object absence.



---



###### Test 19 — Full Combined Scene



Input



**A small red key is inside a locked wooden chest on a large table. The table is near a blue chair, and a candle is behind the chair. When the player opens the chest, the chest becomes unlocked.**



Evaluation



* Objects: Correct
* Multiple attributes: Correct
* Object states: Correct
* Containment: Correct
* Spatial relationships: Correct
* Relationship attachment: Correct
* Interaction trigger: Correct
* State change: Correct
* Overall: Pass



---



##### 4. Overall Evaluation



The evaluation demonstrates that the parser can successfully convert a range of natural-language scene descriptions into the structured JSON representation.

The tested capabilities include:



* Object extraction
* Attribute extraction
* State extraction
* Spatial relationship extraction
* Relationship attachment
* Multiple objects of the same type
* Containment
* Reference resolution
* Interaction triggers and effects
* Multiple effects
* Complex scene descriptions



The evaluation also identified two important limitations:

1. The current representation has limited support for descriptive attributes and an explicitly controlled spatial-relation vocabulary.
2. Negation and object absence are not yet represented cleanly in the current schema.



These limitations are considered future refinement areas rather than blockers for the current prototype.



##### 5\. Conclusion



* The current scene parser is sufficiently functional for the next stage of the prototype.
* The next development stage is to use the structured JSON representation as input to a scene-generation system, moving from:
* Natural-language description

&#x09;→ Structured scene JSON

&#x09;→ Generated game scene

* Further parser refinement can be performed as additional scene-generation requirements are introduced.

