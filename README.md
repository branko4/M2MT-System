# M2MT-system
 Model-To-Model Translation system is designed to offer support for creating and maintaining translations between two or more models.

# Setup Git Repository
The git repository exists out of multiple branches. The most important branches are **main** and **dev/Connect-FE-to-BE**. The main branch will contain the most recent (and most stable) version of the software. The dev/Connect-FE-to-BE branch contains the source code created for the internship and will be kept as it is. Besides the main and dev/Connect-FE-to-BE are there also feature/ development branches. These branches will be prefixed with `dev/`, followed by descriptive name of the activities in that branch.

# Setup Angular

## Project setup
Angular supports the modular architecture pattern. This is both as by having [modules](https://angular.io/guide/ngmodules), as well as having [libraries](https://angular.io/guide/libraries).

### Applications
The current project contains the following applications:
- Mapping; This is the application that can be used to make and view a given mapping.

#### Mapping application setup
The mapping application contains two modules. These are the default app module and the module required for routing. The application has several components and services. Therefore they are separated into two directories (`components` and `service`). The components directory is separated into two directories. One directory is for components that are representing a page (pop-up included), which is the `screen-components` subdirectory. While the other directory have components, that are added and shared between multiple pages. This is the `element-component` directory. Note that if a component is only used by one screen component it is in the subdirectory of that component and not in the `element-component` directory.

##### special classes
The biggest part of the software is inline with Angular, however some parts are going further then Angular. One of these classes is the `MappingService` class. Although it is still an Angular service, there are something added, to keep it better manageable. First of all it is a little bit in a gray area. Since official the components are for visualization and services are there to do the DRY work. However since this is the local app state and changes to the app state means that the mapping service needs to change behavior and the view needs to look different or better said needs to enable specific task depending to the state, the exception has been made to handle both the app state, as well as the visualization of it.

###### MappingService
The Mapping service uses state pattern. Where each activity in the entire state diagram is a function of a state interface and each state is a class that implements the state interface. Therefore the behavior can be easily managed and verified. Since each state behavior is written in it's own class. This will also make the use of state if statements redundant. [State pattern on Wikipedia](https://en.wikipedia.org/wiki/State_pattern)

The implementation of the state pattern is in this case only done halfway, since the state can be changed from outside of the state pattern, however there is only one who holds the state, which is the MappingService. The state transition are done both through an observer pattern a like solution [rx library](https://angular.io/guide/rx-library) and by the mapping service forcing a state transition . By creating a new object and setting the state. The observer pattern alike solution changes the state by given a change event. However the MappingService is still response for changing the state, since it follows the observable.

This could be made more neat by creating a generic State Manager, which takes a generic type state (interface). State interface could extend a `StateInterface`, which has the function `setStateManager(stateManager: StateManagerChanger)`. Where the state manager implements the `StateManagerChanger` interface. The `StateManagerChanger` interface has a function `setCurrentState(stateInterface: StateInterface)`. The implementation will change the current reference to the state. This will make it possible to give the state full access to update the state while the state doesn't know its real container.


**Small side note; *Just though about the fact that there will be a problem with reaching the functions, a solution needs to be found***

### Libraries
The current project contains the following libraries:
- Shared; This is a library that contains more generic components, like tables.

#### Shared library setup
The shared library contains only one module. This module exports several components. The following components are exported by this module:
- conformation-pop-up
- pop-up
- stepper
- table

##### Usage
See mapping application, however it should be as simple as importing the module into the app module. When that is done you should reference the component you want to use.

**Don't forget to prefix the component with lib, when referencing them in a template.**

###### pop-up
To use the pop-up add the content between the pop-up tags.

Outputs:
- backgroundClick; this event will be emitted when the background of the background is clicked

Example:
```HTML
<lib-pop-up>
  <!-- Some html like content -->
</lib-pop-up>
```

###### stepper
This component can be used to move between steps c.q. actions like a create account could have steps like contact info, email conformation, telephone conformation, etc.

**Please note that this component uses some more advanced Angular and TS code, this will not be documented**

Inputs:
- steps: *Step\<any\>*[]; This is a list of steps. A Step is a self defined interface, which require a reference to the component and the data that is required by the component. Since each component might need different data, this is a generic type. However to make sure the data is still correct the step should be created through either a specific function for creating the step or direct reference with proper typing.

Outputs:
- stepsChange: *EventEmitter\<Step\<any\>*[]*\>*; This event is emitted each time a step moves to a different step.

To create a step component you need to extant the step component interface.

###### **Example code:**
**HTML of user**
```HTML
<lib-stepper [(steps)]="steps" (stepsChange)="onFormDataChanged()"></lib-stepper>
```
**TS of user**
```TS
class UserOfStepper {

  steps: Step<any>[] = [
    SelectElementComponent.GetReference({
      input: {
        modelRef: {
          id: "dfgb",
          id2: "dfgb"
        },
      },
      output: {},
    }),
    SelectElementComponent.GetReference({
      input: {
        modelRef: {
          id: "EULYNX",
          id2: "EULYNX"
        },
      },
      output: {},
    }),
    MappingRuleMetaDataComponent.GetReference({
      output: {},
    }),
  ];
}
```


**TS of a step**
```TS
interface ExpectedData {
  // some data
}

class SelectElementComponent implements StepComponent<ExpectedData> {
  @Output() selected = new EventEmitter<{name: string}>()
  data?: ExpectedData;

  static GetReference(data: ExpectedData): Step<ExpectedData> {
    return {
      component: SelectElementComponent,
      data: data,
    };
  }
}
```

###### table
The table is rather simple compart to the stepper.
The table consists out of standard columns and one action columns.

Input:
- data: *Table*; This input is both the (labeling of) columns, rows (actual data), and optional the actions, when actions list is empty, the action column is not shown.

Just follow the given data definition and you will probably be fine. However, the action column might be a bit more difficult.

A action is just a callback function, which means that when an action class is maded, the action class will be the reference environment, since this one will call, c.q. create the function. Therefore the `that` variable is given. Which is a reference to the previous referencing environment.

So to short up. An Action requires three arguments. The first is the name of the action. The second one is a function. The function will be called when the button is pressed. The final one is the reference environment, of which you want the function to have access to.

One better solution would be to replace the function by an observer like solution, for instance using ng rx might be better or to create observer and observable, which is kind a the that variable of the creation.


# Setup backend

## !**Important**!
- Since the backend uses npgsql, don't forget to add/ change the connections string in the `test-env.json` and `appsettings.Development.json`. Which includes:
  - password
  - location
  - username
  - database, since database, if not defined, is equal to username
