# Scope
## Identification
This is the software test report (STR) for the testing of the mapping subsystems. The covered tests are:
- Comprehensive unit tests for the service layer of the Mapping backend
- Functional tests for the Mapping frontend.

## System overview
### general system
The tests were focused on two parts of the Mapping subsystem. The Mapping subsystem is a subsystem of the Model-to-Model-Translation system (abbreviated with M2MT system). The M2MT system includes the following subsystems:
- Mapping subsystem
- Translation definition subsystem
- Translator subsystem
- Manager subsystem

In the current design of the mapping system, there is no communication to other subsystems. However it should be stated that in later designs the mapping subsystem will communicate with the manager subsystem and possibly with the translation definition subsystem.
The mapping subsystem currently contains two (self build) CSCI's and one pre-existing CSCI. The two self build CSCI's are:
- a frontend (Mapping frontend)
- a backend (Mapping backend)

The pre-existing CSCI is a Postgres Database, most often referred to as the database. On which updates to the structure should be made through SQL files (i.e. configuration files) and saved in the M2MT-system repository under /database.

The mapping system is made to run in a Docker environment. This means that the Postgres database runs on the pre-made docker image called postgres. The Mapping frontend is build upon Nginx and is served through static compiled HTML, JavaScript, and CSS files and should be build from its own dockerfile (Dockerfile.frontend). The Mapping backend is just a normal .Net application and is build with the Dockerfile.backend.

### Testing environment
The testing environment consists out of three parts:
- A complete system from end to start (for the functional tests)
- A node Docker container (for Newman tests)
- A xUnit test environment (for unit tests of the Mapping backend)

## Document overview
This report will cover:
- Tests results of the functional tests
- Tests results of the unit tests of the Mapping backend
- Changes/ differences relative to software test plan (STP)

Meaning of used symbols:

| Symbol | meaning |
| ------ | ------- |
| ✅ | Succeeded |
| ❌ | Failed    |
| ⭕ | Failed partly |
| ❓ | Not executed/ Unknown |

# Overview of test results
## Overall assessment of the software tests
Software meets many requirements/use cases. However, it is currently not proven that all requirements and/or use cases (under scope) fully work, 80%.

From the functional requirements/ use cases around 79% of the system is proven to be working. Which is in total 18 functionalities/ use cases are working. The remaining functionalities should be added later on. However the system should be ready to be used, since all critical functionalities are working, except one. The remaining critical functionality is not directly causing problems, however it should be added as the first thing to be added, cause it could create problems in use.

The software does also maintain quite allot of the (business) rules. However it is not proven that all rules indeed implemented, since the tests were not implemented to do so. However the software does maintain at least around 60% of the rules, which is proven by the given test results. However the remaining 40% is not proven, and looking through the code, also not likely to be implemented.

Taking in account that the frontend also helps by inputting the correct information and business logics. The reliability of the software will be high, when the system is used through the graphical user interface (GUI). However during maintains of the code, the reliability can/ will drop if the tests for the service layer are not extended to test the full range of (business) rules. This is cause it is unknow rather a change will start to incorrectly communicate with the backend.

Concluding that the implemented software seems to be working correctly as intended.

**Note; because of the way the tests IO are defined there are unnecessary checks, since if element is not in model of mapping, then we don't even care that they are not in the system :)**

## Impact of test environment
Since there is a difference between the impact of test environments of the functional, Newman, and unit tests, they are separated into there own chapters.

### Impact of test environment for unit tests
The test environment for unit tests is different on the following points:
- Repository layer is faked, and could behavior differently
- The used model is small
- The call is made from test it self and not from controller or HTTP request

This could result in the following problems:
- Repository layer behaved differently and could return/ throw unexpected objects. Which would possibly result in unknow behavior, however more likely in a 500 HTTP error returned to the frontend.
- Since the used model (for testing) is small, it could happen that a larger size model creates a weird state, which causes unknown problems or behavior, which was unforeseen at this time.
- Since a call could be made from another environment it might contain (data)type, which is unknown in the testing environment, but does change behavior for the service. Although it is highly unlikely that this would create a real issue, it should not be overlooked.

### Impact of functional and Newman tests
The test environment for the functional and Newman tests should almost the same as the production environment. The goal of the test environment for the functional and Newman tests is to be the same or as good enough the same as the actual production environment.

## Recommended improvements
The following advisements are made for testing:
- Get Newman tests working
- Add integration tests for backend to database

It is also foreseen that it might be possible to create a testing system/ framework, which could generate tests inputs with a given output. However this might be the same as model based testing, which would indicated that the idea, solution, and effeteness is already know.

The following improvements are recommended for the Mapping system:
- **Fix bug** which causes **only relations between lowest elements** can be **made**
- Add the option add an element to a mapping rule
- Research rather the missing methods are necessary and if so implemented (and test) the missing methods
- Research rather the incorrectly implemented methods (in the wrong class) should be moved to correct class and test those methods

The following improvements are recommended for the next testing fase:
- Improve current test description with the given notes
- Implement all missing test (data), so the testing stage can fully reliable say something about the reliability of the software
- Add test data to check if relations between parent objects can also be made

# Detailed test results
The tests under test scope were:
- use cases tests, which are functional tests
- controller tests, which are Newman integration tests
- Repository tests, which are Xunit unit tests
- Service tests, which are Xunit unit tests

The results gathered for each tests are given in the following paragraphs underneath.

## Use Case (functional) tests

### Summary of test results
These tests are executed by hand and focusses on rather the system functionally is able to do the basics.

| # | Test name | Test Result |
| --- | --- | --- |
| 1. | (Re)load relation; Happy flow | ✅ |
| 2. | Add element; Happy flow  | ❌ |
| 3. | Add relation; Happy flow | ⭕ |
| 4. | Change active element; Happy flow | ❌ |
| 5. | Create mapping rule; Happy flow | ✅ |
| 6. | Create already excisting mapping | ❓ |
| 7. | Create mapping by user; Happy flow | ✅ |
| 8. | Delete element mapping rule; Happy flow | ✅ |
| 9. | Delete mapping; Happy flow | ✅ |
| 10. | Delete relation; Happy flow | ✅ |
| 11. | Delete relation; Happy flow | ✅ |
| 12. | Deload relation; Happy flow | ✅ |
| 13. | Edit element mapping rule; Happy flow | ✅ |
| 14. | Edit mapping; Happy flow | ✅ |
| 15. | Load active element; Happy flow | ✅ |
| 16. | Load active elements; Happy flow | ✅ |
| 17. | Load relation; Happy flow | ✅ |
| 18. | Select element; Happy flow | ✅ |
| 19. | Select model; Happy flow | ✅ |
| 20. | View element mapping rules; Happy flow | ✅ |
| 21. | View elements; Happy flow | ✅ |
| 22. | View mapping rule; Happy flow | ⭕ |
| 23. | View mappings; Happy flow | ✅ |
| 24. | View models; Happy flow | ✅ |

- 5 of 24 tests failed.
- That means around 75% of system is functionally working
- test 6. (*Create already excisting mapping*) should not have been between tests
- Which lowers the number to 4 failing tests
- Which means around 82.6% (19/23*100) of system is functionally working

### Problems encountered
See notes by each test scenario. Overall problem; most of the test descriptions were outdated or incomplete.

### Test scenarios

#### (Re)load relations
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| (Re)load relation; Happy flow | List with relations | All given relations are shown on to the screen | All relations given for mapping rule are shown on screen | ✅ |

#### Add element
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Add element; Happy flow | Reference to model where model has at least 20 elements | There is an element added to mapping rule | No button found that leads to add an option to add element | ❌ |

#### Add relation
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Add relation; Happy flow | Relation that needs to be added to mapping | The relation is add in the system and shown at mapping (rule screen) | On selecting attributes from parent objects 500 error, on selecting onw attributes line between attributes is drawn | ⭕ |

**Bug Note/ TODO/ FIXME; fix bug where it is not possible to add a relation to or from a attribute of a parent element!**

#### Change active element
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Change active element; Happy flow | The new element | System added new element | Other elements are present, however they are not clickable | ❌ |

#### Create element mapping rule
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Create mapping rule; Happy flow | The elements, that needs to be added to mapping rule | New mapping rule is added to system | Select an element from left side, select an model from right side, write name, and model is created | ✅ |

**Note; relations need to be added later on, seems like a description mistake**

#### Create mapping
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Create already excisting mapping | - | - | - | ❓ |
| Create mapping by user; Happy flow | Models that are not the same | User can create a mapping with models | Select a model, select a different model, write name of mapping, mapping is created and saved in the system | ✅ |

**Note; seems that these tests are outdated**

**Note 2; *Create already excisting mapping* is not executed, since only happy flow was goal**

**Note 3; version management is removed from test description *Create mapping by user***

#### Delete element mapping rule
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Delete element mapping rule; Happy flow | (Reference to) Mapping rule that requires to be deleted | Given mapping rule is removed from system | There is a button with name *delete*, when button is pressed there is a popup, popup asks for user to retype a given text, after retyping given text and pressing the delete button in the popup the mapping is removed from the system, **one note the popup z-index is incorrect, since it is of same index of stuff in the background** | ✅ |

**Note; the popup z-index is incorrect, since it is of same index of stuff in the background**

#### Delete mapping
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Delete mapping; Happy flow | System has at least one mapping |  System has deleted the selected mapping | There is a button with name *delete*, on pressing the button popup shows up, popup asks the user to retype given text, after retyping the given text and selecting the button in the popup called *delete* the mapping is removed from the system | ✅ |

#### Delete relation
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Delete relation; Happy flow | Relation that should be removed | Given relation is removed from system | The selected relation, that should be removed, is removed and no longer visible on screen  | ✅ |

#### Deload relation
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Deload relation; Happy flow | Relation that should be removed | Given relation is removed from screen | When a relation should be removed from screen, relation disappears from screen | ✅ |

#### Edit element mapping rule
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Edit element mapping rule; Happy flow | A mapping rule | A user can do multiple CRUD operations on given mapping rule | System contains mapping rule, on selecting the mapping rule the a screen shows the selected mapping rules, gives options to add and remove mapping relations, relations are shown on screen | ✅ |

#### Edit mapping
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Edit mapping; Happy flow | None | A user can do multiple CRUD operations on given mapping | By selecting a mapping, all mapping rules inside of mapping show up, it is possible to add new mapping rules, to remove existing mapping rules, and to preform CRUD operations on existing mapping rules | ✅ |

#### Load active element
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Load active element; Happy flow | An element | System loads given element and relations to other already existing elements | When an element should be loaded in the element is loaded, also the proper relations are loaded with it | ✅ |

#### Load active elements
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Load active elements; Happy flow | Elements that should be loaded | The given elements are loaded and relations between elements are also loaded | When viewing a mapping rule, one element from each model side is loaded in, existing relations are shown on screen  | ✅ |

#### Load relation
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Load relation; Happy flow |Relation that should be added | Given relation is added from screen | When a relation is given to be added on screen, the relation shows up | ✅ |

#### Select element
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Select element; Happy flow | (Reference to) Model, with at least 20 elements | System makes it possible to select one specific element | It is possible to select an element through a taxonomy tree. When element is selected it is also added (on a certain moment) to the mapping rule | ✅ |

**Note; test description is not perfect, first there is a mistake, since officially there is requirement, which is replaced by element in this part of the document. Second it does not clarify what the user should do.**

**Note 2; test description says *at least 20 elements* however given less then 20 elements (10 or something alike) shows bug on rendering the taxonomy tree, with an offset on the second row of the tree**

#### Select model
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| Select model; Happy flow | Selected model | User can select a model | A selector appears containing the known models. When model is selected, the model is shown on screen as a selected item. | ✅ |

**Note; This test description is a bit short, what it should describe is that input is that an user can select one or more models, user selects a model. Where the expected is that the selected model is selected in one way or another.**

**Due to the fact that the test description is not correct the tests is a bit failing, however the functionality seems to work correctly.**

#### Verify mapping
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |


#### View element mapping rules
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| View element mapping rules; Happy flow | A mapping with at least 20 mapping rules | All mapping rules are shown to screen | All mapping rules given for a selected mapping are shown to screen | ✅ |

#### View elements
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| View elements; Happy flow | A (reference to a) model with at least 20 elements | All elements are shown to screen | All elements are shown on screen | ✅ |

**Note; bug with a low amount of elements, on which the second row of the taxonomy tree has an incorrect offset**

#### View mapping rule
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| View mapping rule; Happy flow | A mapping rule, with at least two elements, where between the elements at least one relation exists | Two of the elements in the mapping rule are shown. The other elements are selectable to show instead. Of the two showed elements the existing relations between them are also shown, the relations update when the change of the elements shown on screen | Two elements are shown, existing relations are also shown, however other elements are not selectable, therefore it is also unknown rather relations will update to | ⭕ |

#### View mappings
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| View mappings; Happy flow | System has at least three mappings | System shows list with mappings | A table with all three mappings is shown on screen | ✅ |

#### View models
| Naam | Input | Description/ Expected | Actual | Conclusion |
| --- | --- | --- | --- | --- |
| View models; Happy flow | System has at least three models | System shows list with models to user | Selector list with all three models is shown on screen | ✅ |

## Controllers (integration) tests

### Summary of test results
NA

### Problems encountered
| Problem | Cause | Consequence |
| --- | --- | --- |
| HTTP requests with body failed | Actual reason remains unknown | All tests were node made or executed |

### Test scenarios
NA

## Repository (unit) tests

### Summary of test results
NA

### Problems encountered
| Problem | Cause | Consequence |
| --- | --- | --- |
| Database access not fakable | FakeItEasy only supports to fake interfaces, or at least does not support to fake static methods | This means that the repository layer could **not** be **unit tested** |

### Test scenarios
NA

## Service (unit) tests
Since the unit tests are done in more depth then the test description the results from the unit tests are given in a separate document. This document is '*test_results.html*'. Also the test results could be generated by running the tests again. That could be done by running the M2MT.Test package in Visual Studio or by running it with VSTest.Console.exe.

### Summary of test results
These tests are automatically executed and focusses on rather the service layer follows the defined business logics. In the table (displayed here under) shows the amount of tests failed, succeeded, partly failed, and not executed/ unknown.

| Symbol | Meaning | Amount |
| ------ | ------- | ------ |
| ✅ | Succeeded     | 19 |
| ❌ | Failed        | 2  |
| ⭕ | Failed partly | 1  |
| ❓ | Not executed/ Unknown | 12 |
| NA | Incorrect test | 1 |
| --- | --- | --- |
| NA | Total | 35 |

- 12 Tests were not implemented (correctly), which lowers in one way the reliability of the produced software, however;
  - 3 Tests are related to not implemented functionalities (add element)
  - 3 Tests are related to not existing methods Read (read filtered), therefore it should be checked rather the tests are not implemented by another method and rather the given method should be implemented at all
  - 2 Tests has no test data, that tests rather the functionality works (unknown element), therefore is it unknown rather the method would behavior correctly, which means that **if** it is **_not tested_ latter on it _becomes dangers_**
  - 4 Tests are tested with not the right input (read all), therefore the actual results for the tests are unknown
- 2 Tests are seen as failing, cause they were not implemented in the right class, if they were implemented in the right class, it will change the status from *failed* to *Not executed/ Unknown*
- 1 Test failed partly, cause the test case could have more different test data, therefore the failing partly test could also be seen a succeeded test, with improvement options
- 1 Test should never been a test case, therefore the test is removed.

Concluding That roughly 20 tests of the 34 tests succeeded, 2 tests failed caused by code mismatch, and 12 Tests were never (completely) executed.

### Problems encountered
| Problem | Cause | Consequence |
| --- | --- | --- |
| Striving for the absolute max | Need to make it too good | Creating tests took longer then expected, however results should be more complete and the tests should be better manageable |
| Difficult to map the test results with the test cases (in this document) | Since tests is written like "with this input, this/ one of these output(s) is/are expected", therefore the results of the tests are many-to-many with the test cases | Results are kept outside of this document, since the results can be generated by running the unit tests, therefore is this document only going in  to the use cases and how many unit tests are covering the test cases |

**Note; Test implementation can be found in M2MT.Test package, with folder Shared/Service/\*\*/\*Test.cs**

### Test scenarios

#### MappingCRUDService
| Naam | Input | Description/ Expected | Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Create] Happy flow | A reference to existing model | Gives created object back | 8 Tests | ✅ |
| [Create] Unknown model | A reference to not existing model | Throws an exception that indicates that the given object incorrect is. Were the given reason is model is unknown | 21 Tests | ✅ |
| [Delete] Happy flow | A reference to existing mapping  | Gives removed object back | 2 Tests | ✅ |
| [Delete] Unknown mapping | A reference to not existing mapping | A reference to not existing model | Throws an exception that indicates that the given object incorrect is. Were the given reason is mapping is unknown | 3 Tests | ✅ |

#### MappingReadService
| Naam | Input | Description/ Expected |Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Read (All)] Happy flow | None | Gives list of mappings back | 1 test | ⭕ |
| [Read (All)] Unknwon mapping | Reference to none existing mapping | Throws an exception that indicates that the given object incorrect is. Were the given reason is mapping is unknown | NA | NA |

**Note; mismatch of expected input, since mapping does not have a primary reference, i.e. identifing reference, there is also no filter function for that input, those the test can not be executed**

**Note 2; Since *'[Read (All)] Happy flow'* is now executed for a specific amount there should also be more lower and upper bound data be added, like a test with empty lists.**

#### MappingRelationCRUDService
| Naam | Input | Description/ Expected | Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Create] Happy flow | A reference to existing mapping rule, existing properties, where properties are not the from the same element, are from an element in mapping rule | Gives created object back | 13 | ✅ |
| [Create] Invalid relation (property is from same model) | Two references to two different properties in same model | Throws an exception that indicates that the given object incorrect is. Were the given reason is that it is not allowed to lay relations between the same model | 1 | ✅ |
| [Create] Invalid relation (property is the same) | Two references to same property | Throws an exception that indicates that the given object incorrect is. Were the given reason is that it is not allowed to lay a relation between the same property | 14 | ✅ |
| [Create] Unknown mapping rule | Reference to none existing mapping rule | Throws an exception that indicates that the given object incorrect is. Were the given reason is given mapping rule is unknown | 5 | ✅ |
| [Create] Unknown property (not in mapping rule) | Reference to a unknown property in mapping rule | Throws an exception that indicates that the given object incorrect is. Were the given reason is property is not know in mapping | 2 | ✅ |
| [Create] Unknown property in system | Reference to none existing property | Throws an exception that indicates that the given object incorrect is. Were the given reason is property is unknown | 6 | ✅ |
| [Delete] Happy flow | Reference to existing mapping relation | Gives removed object back | 2 | ✅ |
| [Delete] Unknown relation | Reference to none existing mapping relation | Throws an exception that indicates that the given object incorrect is. Were the given reason is given mapping relation is unknown | 3 | ✅ |

**Note; expected input is incorrect, possibly copy/ paste error, *[Create] Unknown property (not in mapping rule)***
**Note; expected description is incorrect, possibly copy/ paste error, *[Create] Unknown property (not in mapping rule)***

#### MappingRelationReadService
| Naam | Input | Description/ Expected | Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Read (All)] Happy flow | Reference to existing mapping rule | Gives list of mapping relations related to given mapping rule back | 0 Tests\* | ❌ |
| [Read (All)] Unknown mapping rule | Reference to none existing mapping rule | Throws an exception that indicates that the given object incorrect is. Were the given reason is given mapping rule is unknown | 0 Tests\* | ❌ |
| [Read (filtered)] Happy flow | Reference to existing mapping rule, and two existing elements, which also exists in given mapping rule | Gives list of mapping relations back which are related to the given mapping rule and the given elements | 0 Tests\* | ❓ |
| [Read (filtered)] Unknown element | Reference to none existing element | Throws an exception that indicates that the given object incorrect is. Were the given reason is given element is unknown | 0 Tests\* | ❓ |
| [Read (filtered)] Unknown mapping rule | Reference to none existing mapping rule | Throws an exception that indicates that the given object incorrect is. Were the given reason is given mapping rule is unknown\* | 0 Tests | ❓ |

**Note \*; if not mistaken, the method that should be tested by this test cases is also not implemented. Which backs the question rather it is needed. Might be implemented through another way though.**

**Extension note; It is correct, method of Read (all) is implemented in different class, implementing class: *MappingRuleReadService*, should be changed and test cases should be written later on, for now tests of read (all) are expected to have failed.**

#### MappingRuleCRUDService
| Naam | Input | Description/ Expected | Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Add element] Happy flow | Reference  to existing mapping rule and existing element, where element is not (yet) in mapping rule and is an element of a model in the mapping related to mapping rule | Gives changed object back |  0 Tests | ❓ |
| [Add element] Unknown element | Reference to none existing element | Throws an exception that indicates that the given object incorrect is. Were the given reason is given element is unknown | 0 Tests | ❓ |
| [Add element] Unknown mapping rule | Reference to none existing mapping rule | Throws an exception that indicates that the given object incorrect is. Were the given reason is given mapping rule is unknown | 0 | ❓ |
| [Create] Happy flow | Reference to existing mapping and two existing elements, where both elements are in models of which the models are in the given mapping | Gives created object back | 6 Tests | ✅ |
| [Create] Unknown element (in mapping) | Reference to elements that are not in models of the mapping   | Throws an exception that indicates that the given object incorrect is. Were the given reason is given element is not know in models of mapping | 0 Tests | ❓ |
| [Create] Unknown element (not excisting) | Reference to none existing elements | Throws an exception that indicates that the given object incorrect is. Were the given reason is given element is unknown | 0 Tests | ❓ |
| [Create] Unknown mapping | Reference to none existing mapping | Throws an exception that indicates that the given object incorrect is. Were the given reason is given mapping is unknown | 4 Tests | ✅ |
| [Delete] Happy flow | Reference to existing mapping rule | Gives removed object back | 1 Test | ✅ |
| [Delete] Unknown mapping rule | Reference to none existing mapping rule | Throws an exception that indicates that the given object incorrect is. Were the given reason is given mapping rule is unknown | 3 | ✅ |

**Note; *Add element* is implemented, however there were no tests found, therefore it is unknow rather the function would fail or Succeed.**

#### MappingRuleReadService
| Naam | Input | Description/ Expected | Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Read (All)] Happy flow | Reference to existing mapping | Gives list of mapping relations related to given mapping rule back | 0 Tests | ❓ |
| [Read (All)] Unknown mapping | Reference to none  existing mapping  | Throws an exception that indicates that the given object incorrect is. Were the given reason is given mapping is unknown | 0 Tests | ❓ |

**Note; There is one tests for the get all function, however that tests does not tests with the proper inputs, therefore zero tests were found.**

#### AttributeReadService
| Naam | Input | Description/ Expected | Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Read (All)] Happy flow | Reference to existing element | Gives list of attributes related to given element back | 0 Tests | ❓ |
| [Read (All)] Unknown element | Reference to none existing element | Throws an exception that indicates that the given object incorrect is. Were the given reason is given element is unknown | 0 Tests | ❓ |

#### ElementReadService
| Naam | Input | Description/ Expected | Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Read (All)] Happy flow | Reference to existing model | Gives list of elements related to given model back | 1 Tests | ✅ |
| [Read (All)] Unknown model | Reference to none existing model | Throws an exception that indicates that the given object incorrect is. Were the given reason is given model is unknown | 2 Tests | ✅ |

#### InformationModelReadService
| Naam | Input | Description/ Expected | Amount of tests | Conclusion |
| --- | --- | --- | --- | --- |
| [Read (All)] Happy flow | none | Gives list of (all) models known to the system back | 1 Tests | ✅ |
