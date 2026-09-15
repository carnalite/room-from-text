### Parsing Experiment Results

#### Test 01



##### Input

A small candle is on a large table near a locked chest.



##### Expected Representation

See `expected_scene_01.json`.



##### Model Output

```json
{
  "objects": [
    {
      "id": "candle_1",
      "type": "candle",
      "size": "small"
    },
    {
      "id": "table_1",
      "type": "table",
      "size": "large"
    },
    {
      "id": "chest_1",
      "type": "chest",
      "state": "locked"
    }
  ],
  "relationships": [
    {
      "type": "on",
      "subject": "candle_1",
      "object": "table_1"
    },
    {
      "type": "near",
      "subject": "table_1",
      "object": "chest_1"
    }
  ],
  "interactions": []
}
```

##### Evaluation

* Objects: Correct
* Object properties: Correct
* Relationships: Partially correct
* Schema compliance: Incorrect
* Hallucinated information: No
* Overall: Partially correct



##### Notes

* The model correctly identified all objects and their explicitly stated properties.
* The `on` relationship was correctly identified as candle → on → table.
* However, the `near` relationship was incorrectly assigned to table → near → chest instead of candle → near → chest.
* The model also used `type` and `object` for relationship fields instead of the defined `relation` and `target` fields.

