##### Prototype End-to-End Results



The prototype was tested using natural-language scene descriptions passed

through the LLM scene parser and into Unity scene generation.



###### Test 1 — Basic Object and Spatial Relation



Input:  

**"A small red candle is on a large table."**



Expected:

A small red candle should be generated on a large table.



Result:  

Passed.



\---



###### Test 2 — Multiple Objects and Attributes



Input:  

**"A small purple key is on a large wooden table near a large chest."**



Expected:  

The parser should identify the key, table, and chest, including their

attributes and spatial relationships.



Result:  

Passed



\---



###### Test 3 — Behind Relationship



Input:  

**"A small red candle is behind a large chest."**



Expected:  

The candle should be generated behind the chest.



Result:  

Passed



\---



###### Test 4 — Containment and State



Input:

**"A small red key is inside a large locked chest."**



Expected:  

The key should be generated inside the chest and the chest should have a

locked state.



Result:  

Passed



\---



###### Test 5 — Basic Interaction



Input:  

**"A large locked chest."**



\*\*Expected:\*\*  

A locked chest should be generated and clicking the chest should change its

state.



\*\*Result:\*\*  

Passed



\---



###### Observations



The end-to-end prototype successfully demonstrates the pipeline from natural-language scene descriptions to structured LLM output and Unity scene generation.



The current implementation uses Unity primitives and approximate spatial placement. More complex spatial reasoning, richer object representations, and more advanced interaction rules remain future work.

